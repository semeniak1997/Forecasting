DECLARE @n int,
@sumOil float,
@sumOil2 float,
@sumCaprolactam2 bigint,
@sumPolyamid2 bigint,

@sumBenzene float,
@sumOilBenzene float,
@aOilBenzene float,
@bOilBenzene float,

@sumCaprolactam bigint,
@sumOilCaprolactam float,
@aOilCaprolactam float,
@bOilCaprolactam float,

@sumPolyamid bigint,
@sumOilPolyamid float,
@aOilPolyamid float,
@bOilPolyamid float,

@sumBCF float,
@sumCaprolactamBCF float,
@aCaprolactamBCF float,
@bCaprolactamBCF float,
@sumPolyamidBCF float,
@aPolyamidBCF float,
@bPolyamidBCF float


--  Рассчеты в качесвте Х
SET @n = (SELECT COUNT([Oil]) FROM [Europe_Table]);
SET @sumOil = (SELECT SUM([Oil]) FROM [Europe_Table]);
SET @sumOil2 = (SELECT SUM(POWER([Oil], 2)) FROM [Europe_Table]);
SET @sumCaprolactam2 = (SELECT SUM(POWER([Caprolactam], 2)) FROM [Europe_Table]);
SET @sumPolyamid2 = (SELECT SUM(POWER([Polyamid], 2)) FROM [Europe_Table]);

-- Регрессия Бензол на Нефть
SET @sumBenzene = (SELECT SUM([Benzene]) FROM [Europe_Table]);
SET @sumOilBenzene = (SELECT SUM([Oil] * [Benzene]) FROM [Europe_Table]);
SET @bOilBenzene = (@n * @sumOilBenzene - @sumOil * @sumBenzene) / (@n * @sumOil2 - @sumOil * @sumOil);
SET @aOilBenzene = (@sumBenzene - @bOilBenzene * @sumOil) / @n;

-- Регрессия Капролактам на нефть
SET @sumCaprolactam = (SELECT SUM([Caprolactam]) FROM [Europe_Table]);
SET @sumOilCaprolactam = (SELECT SUM([Oil] * [Caprolactam]) FROM [Europe_Table]);
SET @bOilCaprolactam = (@n * @sumOilCaprolactam - @sumOil * @sumCaprolactam) / (@n * @sumOil2 - @sumOil * @sumOil);
SET @aOilCaprolactam = (@sumCaprolactam - @bOilCaprolactam * @sumOil) / @n;

-- Регрессия Полиамид-6 на нефть
SET @sumPolyamid = (SELECT SUM([Polyamid]) FROM [Europe_Table]);
SET @sumOilPolyamid = (SELECT SUM([Oil] * [Polyamid]) FROM [Europe_Table]);
SET @bOilPolyamid = (@n * @sumOilPolyamid - @sumOil * @sumPolyamid) / (@n * @sumOil2 - @sumOil * @sumOil);
SET @aOilPolyamid = (@sumPolyamid - @bOilPolyamid * @sumOil) / @n;

 -- Регрессия BCF на Капролактам
SET @sumBCF = (SELECT SUM([BCF]) FROM [Europe_Table]);
SET @sumCaprolactamBCF = (SELECT SUM([Caprolactam] * [BCF]) FROM [Europe_Table]);
SET @bCaprolactamBCF = (@n * @sumCaprolactamBCF - @sumCaprolactam * @sumBCF) / (@n * @sumCaprolactam2 - @sumCaprolactam * @sumCaprolactam);
SET @aCaprolactamBCF = (@sumBCF - @bCaprolactamBCF * @sumCaprolactam) / @n;

-- Регрессия BCF на Полиамид-6
SET @sumPolyamidBCF = (SELECT SUM([Polyamid] * [BCF]) FROM [Europe_Table]);
SET @bPolyamidBCF = (@n * @sumPolyamidBCF - @sumPolyamid * @sumBCF) / (@n * @sumPolyamid2 - @sumPolyamid * @sumPolyamid);
SET @aPolyamidBCF = (@sumBCF - @bPolyamidBCF * @sumPolyamid) / @n;

UPDATE Forecast_Table SET [ForecastBenzeneEurope] = @bOilBenzene * [OilPrise] + @aOilBenzene;
UPDATE Forecast_Table SET [ForecastCaprolactamEurope] = @bOilCaprolactam * [OilPrise] + @aOilCaprolactam;
UPDATE Forecast_Table SET [ForecastPolyamidEurope] = @bOilPolyamid * [OilPrise] + @aOilPolyamid;
UPDATE Forecast_Table SET [ForecastBCFEurope] = ((@bCaprolactamBCF * [ForecastCaprolactamEurope] + @aCaprolactamBCF) + (@bPolyamidBCF * [ForecastPolyamidEurope] + @aPolyamidBCF)) / 2;
