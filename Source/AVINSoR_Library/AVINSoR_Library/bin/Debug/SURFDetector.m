% http://uk.mathworks.com/help/vision/examples/image-search-using-point-features.html#zmw57dd0e863
% http://uk.mathworks.com/help/vision/examples/object-detection-in-a-cluttered-scene-using-point-feature-matching.html
% http://uk.mathworks.com/videos/computer-vision-made-easy-81802.html?s_iid=disc_rw_cmv_bod


classdef SURFDetector
	properties (GetAccess = public, SetAccess = public)
		refImg;
		testImg;
		PercentageThreshold = 15; % returns false if # of points < this percentage.
		TrainMeInMATLAB = false;
	end;
	
	
    properties (GetAccess = public, SetAccess = private)
		% webcam
		cam;
		cam_name = 'Logitech HD Webcam C270';
		cam_resolution = '800x600';
		
		% reference images and SURF data
		refImgGrey;
		refFeatures;
		refValidPoints;
		
		% last captured frame and SURF data
		testImgGrey;
		testFeatures;
		testValidPoints;

        % last compare results
		PointsMatched; % Number of points matched from last 'test'.
		PointsMatchedPercent; % Percentage representation of the above.
		PolygonPoints; 
		PolygonArea; 
		
		% RESULT output classifiers
		percentageMatchClassifier;
		polygonAreaClassifier;
		CategoryResult; %% 1 is NO MATCH, 2 is MATCH
		ObjectFound;
		
		% object state properties
		WaitingForTrainingResponse = false;
	end
	
	
	methods (Access = public)
		function obj = SURFDetector(refImg)
			%% Use existing reference image if present
			if ~isfloat(refImg)
				obj.refImg = refImg;
				obj.refImgGrey = rgb2gray(refImg);
			end
			
			% Initialize boolean 'detection' output classifiers 
			obj.percentageMatchClassifier = Bayesian(2, [0:100]);
			obj.polygonAreaClassifier = Bayesian(2, [0: 999999]); 
			obj.polygonAreaClassifier.beta = 20000;
		
			obj = obj.startWebcam();
		end
		
		% start the webcam and show preview
		function obj = startWebcam(obj)
			obj.cam = webcam(cam_name);
			obj.cam.Resolution = cam_resolution;
			preview(obj.cam);
		end
		
		% stop the webcam and close preview window
		function obj = stopWebcam(obj)
			delete(obj.cam);
			obj.cam;
		end
		
		% capture a reference image and train the detector
		function obj = captureRefImg(obj)
			obj.refImg = snapshot(obj.cam);
			obj.refImgGrey = rgb2gray(obj.refImg);
			obj = findSURFPoints(obj);
		end
		
		% capture reference image, allow cropping, and train the detector
		function obj = captureRefImgAndCrop(obj)
			wholeImage = snapshot(obj.cam);
			figure;
			axesHandle = axes;
			imshow(wholeImage);
			title('Query Image') % Note that once the bounding box is displayed, you can move it with your mouse. % For example, try choosing the staple remover.
			rectangleHandle = imrect(axesHandle,[130 175 330 365]); % chooses the elephant
			pause;
			obj.refImg = imcrop(wholeImage,getPosition(rectangleHandle));
			obj.refImgGrey = rgb2gray(obj.refImg);
			% find SURF points
			obj = obj.findSURFPoints();
		end
		
		% capture image, compare image SURF features, and train classifier 
		function obj = captureCompareTrain(obj)
			obj.testImg = snapshot(obj.cam);
			obj.testImgGrey = rgb2gray(obj.testImg);
			obj = compare(obj);
			% update categorization result
			obj = updatecategorizationResult(obj);
			% train!
			WaitingForTrainingResponse = true;
		end
		
		% capture image, compare image SURF features, and train classifier 
		function obj = captureCompare(obj)
			obj.testImg = snapshot(obj.cam);
			obj.testImgGrey = rgb2gray(obj.testImg);
			obj = compare(obj);
			% update categorization result
			obj = updatecategorizationResult(obj);
		end
		
		% compare existing image SURF features
		function obj = compareImg(obj, img)
			obj.testImg = img;
			obj.testImgGrey = rgb2gray(obj.testImg);
			obj = compare(obj);
			% update categorization result
			obj = updatecategorizationResult(obj);
        end
		
		% compare existing image SURF features
		function obj = compareTrainImg(obj, img)
			obj.testImg = img;
			obj.testImgGrey = rgb2gray(obj.testImg);
			obj = compare(obj);
			% update categorization result
			obj = updatecategorizationResult(obj);
			% train!
			WaitingForTrainingResponse = true;
        end
		
		% train the classifiers
		function obj = trainClassifierInMatlab(obj)
			%close all;
			isMatch = input('Did I detect the object?');
			if isMatch == 0 % no match
				obj.percentageMatchClassifier = obj.percentageMatchClassifier.createAssociation(obj.PointsMatchedPercent, 1);
				obj.polygonAreaClassifier = obj.polygonAreaClassifier.createAssociation(obj.PolygonArea, 1);			
			elseif isMatch == 1 % MATCH!
				obj.percentageMatchClassifier = obj.percentageMatchClassifier.createAssociation(obj.PointsMatchedPercent, 2);
				obj.polygonAreaClassifier = obj.polygonAreaClassifier.createAssociation(obj.PolygonArea, 2);			
			end
			close all;
			%% RESET flag
			WaitingForTrainingResponse = false;
		end

		% train the classifiers from external application - 0 indicates NO MATCH, 1 indicates MATCH
		function obj = trainClassifier(obj, wasDetectionSuccessful)
			isMatch == wasDetectionSuccessful;
			if isMatch == 0 % No Match
				% 1 is no match, 2 is match
				obj.percentageMatchClassifier = obj.percentageMatchClassifier.createAssociation(obj.PointsMatchedPercent, 1);
				obj.polygonAreaClassifier = obj.polygonAreaClassifier.createAssociation(obj.PolygonArea, 1);			
			elseif isMatch == 1 % MATCH!
				% 1 is no match, 2 is match
				obj.percentageMatchClassifier = obj.percentageMatchClassifier.createAssociation(obj.PointsMatchedPercent, 2);
				obj.polygonAreaClassifier = obj.polygonAreaClassifier.createAssociation(obj.PolygonArea, 2);			
			end
			close all;
			%% RESET flag
			WaitingForTrainingResponse = false;
		end
		
		% 
		function obj = findSURFPoints(obj)
            obj.refImgGrey = rgb2gray(obj.refImg);
			refPoints = detectSURFFeatures(obj.refImgGrey);
			[obj.refFeatures,  obj.refValidPoints] = extractFeatures(obj.refImgGrey,  refPoints);
			sprintf('%i valid SURF points.', length(obj.refValidPoints))
			% show training results
			figure; imshow(obj.refImg);
			hold on; plot(refPoints.selectStrongest(50));			
		end
	end
	
	
	
	methods (Access = private)
		% Compare the test image and it's SURF-features (and geometric locations) with that of reference image.
		% Sourced from MATLAB(R) SURF-Feature examples
		function obj = compare(obj)
			% Detect features
			testPoints = detectSURFFeatures(obj.testImgGrey);
			[obj.testFeatures, obj.testValidPoints] = extractFeatures(obj.testImgGrey, testPoints);
			figure;imshow(obj.testImg);
			hold on; plot(testPoints.selectStrongest(50));
			
			%% Compare card image to video frame
			index_pairs = matchFeatures(obj.refFeatures, obj.testFeatures);
			
			refMatchedPoints = obj.refValidPoints(index_pairs(:,1)).Location;
			testMatchedPoints = obj.testValidPoints(index_pairs(:,2)).Location;
			obj.PointsMatched = length(index_pairs);
			sprintf('%i matched SURF points.', obj.PointsMatched)
			pointsMatchedPercent = round((obj.PointsMatched/length(obj.refValidPoints))*100);
			obj.PointsMatchedPercent = pointsMatchedPercent;
			sprintf('%i percent of all possible SURF points matched.', obj.PointsMatchedPercent)
			
			figure, showMatchedFeatures(obj.testImg, obj.refImg, testMatchedPoints, refMatchedPoints, 'montage');
			title('Showing all matches');
			
			%% Define Geometric Transformation Objects
			gte = vision.GeometricTransformEstimator; 
			gte.Method = 'Random Sample Consensus (RANSAC)';

			[tform_matrix, inlierIdx] = step(gte, refMatchedPoints, testMatchedPoints);

			ref_inlier_pts = refMatchedPoints(inlierIdx,:);
			I_inlier_pts = testMatchedPoints(inlierIdx,:);

			% Draw the lines to matched points
			figure;showMatchedFeatures(obj.testImg, obj.refImg, I_inlier_pts, ref_inlier_pts, 'montage');
			title('Showing match only with Inliers');

			%% Transform the corner points 
			% This will show where the object is located in the testImg

			tform = maketform('affine',double(tform_matrix));
			[width, height,~] = size(obj.refImg);
			corners = [0,0;height,0;height,width;0,width];
			new_corners = tformfwd(tform, corners(:,1),corners(:,2))
			
            % store geometric coordinates of detected object
            obj.PolygonPoints = new_corners;
			
			% calculate polygon area
            obj.PolygonArea = round(polyarea(obj.PolygonPoints(:,1), obj.PolygonPoints(:,2)));
            
			% produce a TRUE/FALSE output
			obj = obj.getResult()
			
			figure;imshow(obj.testImg);
			patch(new_corners(:,1),new_corners(:,2),[0 1 0],'FaceAlpha',0.5);
		end		
		
		
		% Get the output as a category
		function obj = updatecategorizationResult(obj)
			% actions array
			a1 = obj.percentageMatchClassifier.getAction(obj.PointsMatchedPercent);
			a2 = obj.polygonAreaClassifier.getAction(obj.PolygonArea);
			actions = [a1 a2];
			disp(actions)
			% risks array
			r = zeros(1,2);
			if a1 ~= 0
				r(1) = obj.percentageMatchClassifier.risks(a1, obj.PointsMatchedPercent); %r_ax
			end

			if a2 ~= 0
				r(2) = obj.polygonAreaClassifier.risks(a2, obj.PolygonArea); %r_ay
			end
			disp(r)
			
			% find the categorization result and update
			if length(unique(actions)) == 1
				obj.CategoryResult = actions(1); % if number of unique action = 1 then all indicate same action, hence return the first decision
            else
				[M, ~ ,C] = mode(actions); 
				C = cell2mat(C);
				if length(C) == 1
					obj.CategoryResult = M; % if not more than one mode, return mode 
				else % else get the first decision that has the lowest risk value present
					[~,minrsk] = min(r); % get action-index of action 
					obj.CategoryResult = actions(minrsk); % return the corresponding decision 
				end
			end
			
			% update binary result
			if (obj.CategoryResult == 2)
			obj.ObjectFound = true;
			else
			obj.ObjectFound = false;
			end
		end
	end
end
