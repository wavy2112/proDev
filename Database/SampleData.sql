begin
DECLARE @g geography;

/* 
Insert a core entity 
with multiple polygons
*/

SET @g = geography::STMPolyFromText('MULTIPOLYGON(((-122.358 47.653, -122.348 47.649, -122.358 47.658, -122.358 47.653)), ((-122.341 47.656, -122.341 47.661, -122.351 47.661, -122.341 47.656)))', 4326);
SELECT @g.ToString();

INSERT INTO [dbo].[COREENTITY]
           ([NAME]
           ,[GEOPOLY]
           ,[DATEBEGIN]
           ,[DATEEND])
     VALUES
           ('Multiple Polys TestEntity'
           ,geography::STMPolyFromText('MULTIPOLYGON(((-122.358 47.653, -122.348 47.649, -122.358 47.658, -122.358 47.653)), ((-122.341 47.656, -122.341 47.661, -122.351 47.661, -122.341 47.656)))', 4326)
           ,GETUTCDATE()
           ,DATEADD(YEAR, 3, GETUTCDATE()))

end
