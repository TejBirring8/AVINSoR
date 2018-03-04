% MATLAB OOP --> https://yagtom.googlecode.com/svn/trunk/html/objectOriented.html#12


classdef Bayesian
	properties (GetAccess = public, SetAccess = public)
		% ** SETTINGS				 **
		% * Beta					  *
		% Integral Breadth to use when producing Gaussian Bell-Curve
		beta = 20; 
	end 
	
    properties (GetAccess = public, SetAccess = private)
		% * Number of Categories 	  *
		noOfCats;
		% Reciprocal of noOfCats i.e. 1/noOfCats 
		recip_noOfCats;
		% * All Input Values	 	  *
		inputVals;
		noOfInputVals;
		% * Priories 			 	  *
		% Prior Probabilities for all categories
		priories;
		% * Likelihoods 		 	  *
		% Class-Conditional Probabilities for all categories
		likelihoodsBin;
		likelihoods;
		% * Evidence			 	  *
		% Scale-Factor
		evidence;
		% * Probabilities of N from X *
		probabilities;
		% * Possible Actions		  *
		%possibleActions;
		% * Risks					  *
		risks;
		% * Appropriate Actions		  *
		actions;
		
		
		% * CLASSIFIER STATE		  *
		% ready = 1 AND notReady = 0
		state = 0;
	end

	methods (Access = public)
		% * CONSTRUCTOR 			  *
		function obj = Bayesian(noOfCategories, inputValsArray)
			obj.noOfCats = noOfCategories;
			obj.recip_noOfCats = 1/obj.noOfCats;
			
			obj.inputVals = inputValsArray;
			obj.noOfInputVals = length(obj.inputVals);
			
			% set default priories, likelihoods, evidence, probabilities, risks, actions, etc.
			obj.priories = 1:1:obj.noOfCats;
			obj.priories(:) = obj.recip_noOfCats;	
			
            obj.risks = zeros(obj.noOfCats, obj.noOfInputVals);
            
			obj.likelihoodsBin = zeros(obj.noOfInputVals, obj.noOfCats);
			obj = obj.resetMatrices();
		end
		% * E.O. CONSTRUCTOR 		  *

		
		% find the nearest value to the input value where the likelihood of occurrence of the specified category 
		% is at least 95%.
		function result = findNearestMaxProbabilityValue(obj, currentValue, categoryOfInterest)
			index_probabilityOf1 = find(obj.probabilities(:, categoryOfInterest) >= .95);
			difference = abs(index_probabilityOf1-currentValue);
			[~, I] = min(difference);
			value = index_probabilityOf1(I);
			if isempty(value)
				result = 0;
			else
				result =  index_probabilityOf1(I);
			end;
		end
		
		
		% * ASSOCIATE VALUE WITH CAT  *    ## Improve for efficiency and versatility w/ different sensor inputs ##
		% Associates an input value with likelihood of association with a particular category, bell-curve peaks at value (highest amplitude possible)
		function obj = createAssociation(obj, value, category)
			obj.state = 0;
			
			% Add Peak @ Value for particular Category - in Binary Table
			obj.likelihoodsBin(value, category) = 1;
			
			% * Generate entirely new Likelihood Probability Densities 
			% Reset/Zero Matrices
			obj = obj.resetMatrices();
			% Generate New Likelihoods
			obj = obj.createLikelihoodsMatrix();
			% Update all other matrices!
			obj = obj.updateAll();
        end % EO createAssociation()
		% * E.O. CREATE ASSOCIATION   *
		
		% * SET PRIOR PROBABILITY 	  *
		% Sets Prior Probability of occurrence for a particular category
		function obj = setPriorProb(obj, category, probability)
			obj.state = 0;
			obj.priories(category) = probability;
			obj = obj.updateAll();
			obj = createLikelihoodsMatrix()
        end % EO createAssociation()
		% * E.O. SET PRIOR PROB.	  *
		
		% * GET ACTION *
		function y = getAction(obj, value)
			y = obj.actions(value);
		end
	end % E.O. Public Methods
    
	
	methods (Access = private)
		function obj = updateAll(obj)
			obj = obj.generateEvidenceArray();
			obj = obj.generateProbsArray();
			obj = obj.generateRisksArray();
			obj = obj.generateActionsArray();
		end
		
		function [y] = generateBellCurve(inputVals, peakVal, amplitude, integralBreadth)
			y = gaus(inputVals, peakVal, amplitude, integralBreadth);
		end
		
		function obj = appendLikelihood(obj, category, newBellCurve)
			obj.likelihoods(:, category) = max(obj.likelihoods(:, category), transpose(newBellCurve));
		end
		
		function obj = generateEvidenceArray(obj)
			%for(category=1:obj.noOfCats)
			%	evidence = evidence + transpose(likelihoods(:, category) * priories(category));
			%end;
			
			l_x_p = repmat(obj.priories, obj.noOfInputVals, 1);
			l_x_p = l_x_p .* obj.likelihoods;
			t_l_x_p = transpose(l_x_p);
			obj.evidence = sum(t_l_x_p);
		end
		
		function obj = generateProbsArray(obj)
			l_x_p = repmat(obj.priories, obj.noOfInputVals, 1);
			l_x_p = l_x_p .* obj.likelihoods;
			t_evidence = transpose(obj.evidence);
			p_x = repmat(t_evidence, 1, obj.noOfCats);
			obj.probabilities = l_x_p ./ p_x;
		end
		
		function obj = generateRisksArray(obj)
			% let i = category index: from 1 to noOfCats
			% let j = action index: from 1 to noOfCats
			delta = diag(ones(1, obj.noOfCats));
			delta = 1-delta;
			
			for i=1:obj.noOfCats
				obj.risks(:, i) = sum(transpose(repmat(delta(i, :), obj.noOfInputVals, 1) .* obj.probabilities));
			end;
			
		end
		
		function obj = generateActionsArray(obj)
				obj.risks=transpose(obj.risks); % so we get a list of Minimums (of all f(X, Ns) for each X
				[~,I] = min(obj.risks);
				obj.actions = I;
		end
		
		function obj = resetMatrices(obj)
			obj.likelihoods = zeros(obj.noOfInputVals, obj.noOfCats);
			obj.evidence = zeros(1, obj.noOfInputVals);
			obj.probabilities = zeros(obj.noOfInputVals, obj.noOfCats);
			obj.risks = zeros(obj.noOfInputVals, obj.noOfCats); 
			% inputVal, Ai
			obj.actions = zeros(1, obj.noOfInputVals);
		end
		
		function obj = createLikelihoodsMatrix(obj)
			for cat=1:obj.noOfCats
				for val=1:obj.noOfInputVals
					% look in binary conditional probability table, and get values at which absolute association with w_x.
					if (obj.likelihoodsBin(val, cat) == 1)
						% ensures amplitude=1/noOfCats for values of X that share absolute association with other categories, else amplitude = 1;
                        if (sum(obj.likelihoodsBin(val, :)) <= 1)
							newBellCurve = gaus(obj.inputVals, val, 1, obj.beta);
							obj.likelihoods(:, cat) = max(obj.likelihoods(:, cat), transpose(newBellCurve));
							%obj = obj.appendLikelihood(cat, newBellCurve);
                        else
                            newBellCurve = gaus(obj.inputVals, val, obj.recip_noOfCats, obj.beta);
							obj.likelihoods(:, cat) = max(obj.likelihoods(:, cat), transpose(newBellCurve));
							%obj = obj.appendLikelihood(cat, newBellCurve);
                        end
                    end
                end
            end	
		end
	end % E.O. Private Methods
    

end

