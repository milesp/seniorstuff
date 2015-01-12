CREATE TABLE [dbo].[Employee]
(
	[EmployeeID] INT NOT NULL PRIMARY KEY, 
    [WorkPeriodID] INT NOT NULL, 
    [Name] CHAR(30) NULL, 
    [Wage] FLOAT NULL, 
    [Experience] FLOAT NULL, 
    [MinimumShifts ] INT NULL, 
    [MaximumShifts] INT NULL, 
    [AssignedShifts] INT NULL, 
    [AvailabilityCoefficient] FLOAT NULL, 
    [ShiftsAllowedPerDay] INT NULL
)
