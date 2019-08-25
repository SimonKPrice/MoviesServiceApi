


MERGE INTO dbo.RegisteredUsers AS Target
USING (
VALUES
(N'John', N'Bond'),
(N'Paul', N'Simmonds'),
(N'Claire', N'Williams'),
(N'Sarah',N'Porter')
)
As Source (Firstname, Lastname)
ON Target.Firstname = Source.Firstname AND Target.Lastname = Source.Lastname

WHEN MATCHED THEN
	UPDATE SET Firstname = Source.Firstname, Lastname=Source.Lastname

WHEN NOT MATCHED THEN
	INSERT (Firstname, Lastname)
	VALUES (Firstname, Lastname);

GO


 