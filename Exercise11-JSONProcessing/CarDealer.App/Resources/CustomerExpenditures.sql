SELECT
	cu.[Name] AS CustomerName,
	SUM(p.Price) AS TotalExpendituresWithoutDiscounts,
	SUM(p.Price - p.Price * s.Discount) AS TotalExpendituresWithDiscounts
FROM Customers AS cu
JOIN Sales AS s
ON s.Customer_Id = cu.Id
JOIN Cars AS ca
ON ca.Id = s.Car_Id
JOIN PartCars AS pc
ON pc.Car_Id = ca.Id
JOIN Parts AS p
ON p.Id = pc.Part_Id
WHERE cu.Id = 30 --> SUBSTITUTE WITH DESIRED ID TO CHECK EXPENDITURES FOR THAT CUSTOMER
GROUP BY cu.[Name]