#IGS plant growing schedule generator
Schedule generator should assept json with trayNumber,recipeName and startDate
it should use external rest api as source of recipes and generates schedulere json with exect time to turn on light and what intencity and what time to turn on watter and what amount to use.


Sample of input json 
```typescript
{
  input: [
    {
      trayNumber: 1,
      recipeName: "Basil",
      startDate: "2022-01-24T12:30:00.0000000Z"
    },
    {
      trayNumber: 2,
      recipeName: "Strawberries",
      startDate: "2021-13-08T17:33:00.0000000Z"
    },
    {
      trayNumber: 3,
      recipeName: "Basil",
      startDate: "2030-01-01T23:45:00.0000000Z"
    }
  ]
}
```
#Assupmtions
According to result of Recipes API json litght phases duration are not correlate with water phase duration 
that why schedule should containe separate rows and time for light and watering operations.

An operation startDate is required. There is no information about necessity of operation endDate. 
It has sense for light and has less sense for water. 
Anyway I will add endDate field into schedule to make more obvious when each operation should end. 

#Expected result json schema
{
	[ trayNumber:"integer",
	  lightSchedule:[ 
	  {
		:"yyyy-mm-ddThh:mm:cc.0000000Z",
		endDate:":"yyyy-mm-ddThh:mm:cc.0000000Z",
		intencity:"integer"		
	  }],
	  waterSchedule:[ 
	  {
		startDate:":"yyyy-mm-ddThh:mm:cc.0000000Z",
		endDate:":"yyyy-mm-ddThh:mm:cc.0000000Z",
		amount:"decimal"
	  }]	  
	]
}

According to my understandin startDate ,endDate and intencity/amount is a minimum set of field required to make a shedulere.

#How to generate a schedule:
Input containe trayNumber,recipeName,startDate.
We could use recipeName to found list of phases and operations in Recipes
Recipe return a list of phases with nested list of operations separately for light and water.
We need to go thought list and  add startDate to operation duration for each operation , repeate the process for repetitions value.

Phase hours,minutes help calculate last operation in a phase duration.
