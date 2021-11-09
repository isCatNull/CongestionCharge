# CongestionCharge
config.json lets user easily adjust rates/time periods/vehicles for congestion charge.

# To test
The library does not contain UI. Therefore, simply run integration tests. 

# What is going on?

We have charge description taken from configuration file that describes time intervals with their charges.
TimeLogic allows to advance to either Charge end time or trip end time, depending which happens earlier. 
ChargeEventBuilder segments the whole "stay" of the vehicle in the city zone. That way we have charge events timeline, which
is aggregated to AM/PM rates for nice output. 
