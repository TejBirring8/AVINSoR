function [y] = gaus(x, x_peak, a, beta)
%SIG Summary of this function goes here
%   Detailed explanation goes here
y = a*exp(-((x-x_peak).^2)/beta^2);
end
