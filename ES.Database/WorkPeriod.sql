CREATE TABLE [dbo].[WorkPeriod]
(
	[WorkPeriodID] INT NOT NULL PRIMARY KEY, 
    [NumOfShiftsPerDay] INT NOT NULL, 
    [ShiftNamesList] VARCHAR(500) NULL, 
    [WorkDaysPerPeriod] INT NOT NULL, 
    [WorkDayNamesList] VARCHAR(500) NULL
)
