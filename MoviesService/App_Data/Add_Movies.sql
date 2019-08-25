


MERGE INTO dbo.Movies AS Target
USING (
VALUES
(N'Captain Marvel', 2019,135, N'Fantasy/Sci-fi'),
(N'The Farewell',2019,98,N'Drama/Comedy'),
(N'The Dark Knight', 2008,135, N'Drama/Thriller'),
(N'The Avengers',2012,98,N'Fantasy/Sci-fi'),
(N'Raiders of the Lost Ark', 1981,135, N'Fantasy/Action'),
(N'Pulp Fiction',1994,98,N'Drama/Crime'),
(N'Forest Gump', 1994,135, N'Drama/Comedy'),
(N'Black Panther',2018,98,N'Fantasy/Sci-fi')
)
As Source (Title, YearOfRelease, RunningTime, Genre)
ON Target.Title = Source.Title

WHEN MATCHED THEN
	UPDATE SET Title=Source.Title, YearOfRelease=Source.YearOfRelease, RunningTime=Source.RunningTime, Genre=Source.Genre

WHEN NOT MATCHED THEN
	INSERT (Title, YearOfRelease, RunningTime, Genre)
	VALUES (Title, YearOfRelease, RunningTime, Genre);

GO


 