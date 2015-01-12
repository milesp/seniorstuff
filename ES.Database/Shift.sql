CREATE TABLE [dbo].[Shift]
(
	[ShiftID] INT NOT NULL PRIMARY KEY, 
    [WorkPeriodID] INT NOT NULL, 
    [Day] INT NOT NULL, 
    [ShiftNumber] INT NOT NULL, 
    [Vacancies] INT NULL, 
    [EmployeeTotal] INT NOT NULL, 
    [ShiftsPerDay] INT NOT NULL, 
    [Offset] INT NULL, 
    [ExpAvg] FLOAT NULL, 
    [ExpMin] FLOAT NULL, 
    [ExpMax] FLOAT NULL, 
    [WageAvg] NCHAR(10) NULL
)
