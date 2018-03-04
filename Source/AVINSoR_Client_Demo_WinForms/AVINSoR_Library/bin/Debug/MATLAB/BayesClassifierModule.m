classdef BayesClassifierModule
	%%%% PUBLIC GET/SET PROPERTIES %%%%
	properties (GetAccess = public, SetAccess = public)
		%% empty
	end
	%%%% END OF PUBLIC GET/SET PROPERTIES %%%
	
	
	%%%% PUBLIC GET, PRIVATE SET PROPERTIES %%%%
	properties (GetAccess = public, SetAccess = private)
		NoOfCategories = 10;
		InputNodeClassifiers = BayesClassifier.empty; % <-- Stores BayesianClassifier for each individual input node, in order of index
		Lock = false; % <-- indicates client application has finished instantiating and asserting new input nodes.
		Decision; % <-- Decision of last Decide() request.
		%IsBusy = false;
	end
	%%%% END OF PUBLIC GET PROPERTIES %%%%
	
	
	%%%%  PRIVATE PROPERTIES %%%%
	properties (GetAccess = private, SetAccess = private)
		DecisionLogic = 0;
	end
	%%%% END OF PRIVATE PROPERTIES %%%%	
	
	
	%%%% PUBLIC METHODS %%%%
	methods (Access = public)
		% * CONSTRUCTOR
		function obj = BayesClassifierModule(noOfCategories)
			obj.NoOfCategories = noOfCategories;
		end
		% * END OF CONSTRUCTOR
		
		function obj = newInputNode(obj, minVal, maxVal)
			obj.InputNodeClassifiers(length(obj.InputNodeClassifiers) + 1) = BayesClassifier(obj.NoOfCategories, minVal:maxVal);
		end
		
		function obj = lock(obj)
			obj.Lock = true;
		end
		
		function obj = getDecision(obj, values)
			% * no of input classifiers to variable
			noOfInputs = length(obj.InputNodeClassifiers);
			
			%%valuesBuffer = values;
			if(numel(values) == noOfInputs)
			%%% *** BEGIN ***

			% * create array for input-node's action
			decisions = zeros(1, noOfInputs);
			% fill
			for i = 1:noOfInputs
				decisions(i) = obj.InputNodeClassifiers(i).actions(values(i));
			end
			% * create array for input-node's action-risk
			risks = zeros(1, noOfInputs);
			% fill
			for i = 1:noOfInputs
				if(decisions(i) ~= 0)
					risks(i) = obj.InputNodeClassifiers(i).risks(decisions(i), values(i));
				end
			end
			% * get result
			if length(unique(decisions)) == 1 
				obj.Decision = decisions(1); % if only one common action amongst ALL inputs, then return the common unanimous decision
			else
				[M, ~ ,C] = mode(decisions); 
				C = cell2mat(C);
				if length(C) == 1
					obj.Decision = M; % if not more than one mode (one decision clearly more frequently occurring than the rest), then return the mode decision
				else 
					[~,minrsk] = min(risks); 
				obj.Decision = decisions(minrsk); % if more than one mode, return the first 'decision category' associated with the MINIMUM risk.
				end
			end
			
			end
			%%% *** END ***
		end
		
		function obj = associateValuesWithCategory(obj, values, category)
			% * no of input classifiers to variable
			noOfInputs = length(obj.InputNodeClassifiers);
			
			for i = 1:noOfInputs
				obj.InputNodeClassifiers(i) = obj.InputNodeClassifiers(i).createAssociation(values(i), category);
			end
		end
		
		function obj = associateValueWithCategory(obj, inputIndex, value, category)
				obj.InputNodeClassifiers(inputIndex).createAssociation(value, category);
		end
		
		%%function obj = getDecision(obj, inputIndex, value)
		%%	obj.InputNodeClassifiers(inputIndex).actions(value);
		%%end
		
		function obj = setCategoryPrioriesForInput(obj, inputIndex, priorProbs)
			obj.InputNodeClassifiers(inputIndex) = obj.InputNodeClassifiers(inputIndex).setPriorProbs(priorProbs);
		end
		
		function obj = plotAllLikelihoods(obj)
			% * no of input classifiers to variable
			noOfInputs = length(obj.InputNodeClassifiers);
			% plot
			for i=1:noOfInputs
				obj.InputNodeClassifiers(i).plotLikelihoods();
				pause();
			end
		end
	end
	%%%% END OF PUBLIC METHODS %%%%
	
	
	%%%% PRIVATE METHODS %%%%
	methods (Access = private)
	end
	%%%% END OF PRIVATE METHODS %%%%
end
