--1
SELECT ID,
    CASE
		WHEN P_ID IS NULL THEN 'Root'
			WHEN ID IN(SELECT P_ID FROM TREE) THEN 'Inner'
			ELSE 'Leaf'
	END AS TYPE
FROM TREE;

--2
UPDATE SALARY
SET SEX = CASE
	WHEN SEX = 'm' THEN 'f'
	ELSE 'm'
END;
--3
SELECT
    Department.id,
    sum(CASE WHEN month = 'Jan' THEN revenue ELSE null END) AS Jan_Revenue,
    sum(CASE WHEN month = 'Feb' THEN revenue ELSE null END) AS Feb_Revenue,
    sum(CASE WHEN month = 'Mar' THEN revenue ELSE null END) AS Mar_Revenue,
    sum(CASE WHEN month = 'Apr' THEN revenue ELSE null END) AS Apr_Revenue,
    sum(CASE WHEN month = 'May' THEN revenue ELSE null END) AS May_Revenue,
    sum(CASE WHEN month = 'Jun' THEN revenue ELSE null END) AS Jun_Revenue,
    sum(CASE WHEN month = 'Jul' THEN revenue ELSE null END) AS Jul_Revenue,
    sum(CASE WHEN month = 'Aug' THEN revenue ELSE null END) AS Aug_Revenue,
    sum(CASE WHEN month = 'Sep' THEN revenue ELSE null END) AS Sep_Revenue,
    sum(CASE WHEN month = 'Oct' THEN revenue ELSE null END) AS Oct_Revenue,
    sum(CASE WHEN month = 'Nov' THEN revenue ELSE null END) AS Nov_Revenue,
    sum(CASE WHEN month = 'Dec' THEN revenue ELSE null END) AS Dec_Revenue
FROM
    Department
GROUP BY
    id;
--4
SELECT
    employee_id,
    CASE WHEN employee_id % 2 != 0 AND name NOT LIKE 'M%' THEN salary ELSE 0 END AS bonus
FROM
    Employees
ORDER BY
    employee_id;