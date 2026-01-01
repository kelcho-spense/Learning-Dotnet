-- Create the required Student table
CREATE TABLE Student
(
     ID INT PRIMARY KEY,
     FirstName VARCHAR(50),
     LastName VARCHAR(50),
     Gender VARCHAR(50)
)
GO

-- Insert the required test data
INSERT INTO Student VALUES (101, 'Steve', 'Smith', 'Male')
INSERT INTO Student VALUES (102, 'Sara', 'Pound', 'Female')
INSERT INTO Student VALUES (103, 'Ben', 'Stokes', 'Male')
INSERT INTO Student VALUES (104, 'Jos', 'Butler', 'Male')
INSERT INTO Student VALUES (105, 'Pam', 'Semi', 'Female')
GO

-- Query to
-- 1. Retrieve all columns from the Student table
SELECT * FROM Student
-- 2. Order
SELECT * FROM Student ORDER BY FirstName ASC
