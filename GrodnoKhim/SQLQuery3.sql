DECLARE @n int,
@sumOil float,
@sumOil2 float,
@sumCaprolactam2 bigint,
@sumPolyamid2 bigint,
@sumFPT2 bigint,

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

@sumFPT float,
@sumOilFPT float,
@aOilFPT float,
@bOilFPT float,
@sumCaprolactamFPT float,
@aCaprolactamFPT float,
@bCaprolactamFPT float,
@sumPolyamidFPT float,
@aPolyamidFPT float,
@bPolyamidFPT float,

@sumCF float,
@sumOilCF float,
@aOilCF float,
@bOilCF float,
@sumCaprolactamCF float,
@aCaprolactamCF float,
@bCaprolactamCF float,
@sumPolyamidCF float,
@aPolyamidCF float,
@bPolyamidCF float,
@sumFPTCF float,
@aFPTCF float,
@bFPTCF float

--  Рассчеты в качесвте Х
SET @n = (SELECT COUNT([Oil]) FROM [Asia_Table]);
SET @sumOil = (SELECT SUM([Oil]) FROM [Asia_Table]);
SET @sumOil2 = (SELECT SUM(POWER([Oil], 2)) FROM [Asia_Table]);
SET @sumCaprolactam2 = (SELECT SUM(POWER([Caprolactam], 2)) FROM [Asia_Table]);
SET @sumPolyamid2 = (SELECT SUM(POWER([Polyamid], 2)) FROM [Asia_Table]);
SET @sumFPT2 = (SELECT SUM(POWER([FPT], 2)) FROM [Asia_Table]);


-- Регрессия Бензол на Нефть
SET @sumBenzene = (SELECT SUM([Benzene]) FROM [Asia_Table]);
SET @sumOilBenzene = (SELECT SUM([Oil] * [Benzene]) FROM [Asia_Table]);
SET @bOilBenzene = (@n * @sumOilBenzene - @sumOil * @sumBenzene) / (@n * @sumOil2 - @sumOil * @sumOil);
SET @aOilBenzene = (@sumBenzene - @bOilBenzene * @sumOil) / @n;

-- Регрессия Капролактам на нефть
SET @sumCaprolactam = (SELECT SUM([Caprolactam]) FROM [Asia_Table]);
SET @sumOilCaprolactam = (SELECT SUM([Oil] * [Caprolactam]) FROM [Asia_Table]);
SET @bOilCaprolactam = (@n * @sumOilCaprolactam - @sumOil * @sumCaprolactam) / (@n * @sumOil2 - @sumOil * @sumOil);
SET @aOilCaprolactam = (@sumCaprolactam - @bOilCaprolactam * @sumOil) / @n;

-- Регрессия Полиамид-6 на нефть
SET @sumPolyamid = (SELECT SUM([Polyamid]) FROM [Asia_Table]);
SET @sumOilPolyamid = (SELECT SUM([Oil] * [Polyamid]) FROM [Asia_Table]);
SET @bOilPolyamid = (@n * @sumOilPolyamid - @sumOil * @sumPolyamid) / (@n * @sumOil2 - @sumOil * @sumOil);
SET @aOilPolyamid = (@sumPolyamid - @bOilPolyamid * @sumOil) / @n;

-- Регрессия Нить на Нефть
SET @sumFPT = (SELECT SUM([FPT]) FROM [Asia_Table]);
SET @sumOilFPT =  (SELECT SUM([Oil] * [FPT]) FROM [Asia_Table]);
SET @bOilFPT = (@n * @sumOilFPT - @sumOil * @sumFPT) / (@n * @sumOil2 - @sumOil * @sumOil);
SET @aOilFPT = (@sumFPT - @bOilFPT * @sumOil) / @n;

 -- Регрессия Нить на Капролактам
SET @sumCaprolactamFPT = (SELECT SUM([Caprolactam] * [FPT]) FROM [Asia_Table]);
SET @bCaprolactamFPT = (@n * @sumCaprolactamFPT - @sumCaprolactam * @sumFPT) / (@n * @sumCaprolactam2 - @sumCaprolactam * @sumCaprolactam);
SET @aCaprolactamFPT = (@sumFPT - @bCaprolactamFPT * @sumCaprolactam) / @n;

-- Регрессия Нить на Полиамид-6
SET @sumPolyamidFPT = (SELECT SUM([Polyamid] * [FPT]) FROM [Asia_Table]);
SET @bPolyamidFPT = (@n * @sumPolyamidFPT - @sumPolyamid * @sumFPT) / (@n * @sumPolyamid2 - @sumPolyamid * @sumPolyamid);
SET @aPolyamidFPT = (@sumFPT - @bPolyamidFPT * @sumPolyamid) / @n;

-- Регрессия Ткани на нефть
SET @sumCF = (SELECT SUM([CF]) FROM [Asia_Table]);
SET @sumOilCF = (SELECT SUM([Oil] * [CF]) FROM [Asia_Table]);
SET @bOilCF = (@n * @sumOilCF - @sumOil * @sumCF) / (@n * @sumOil2 - @sumOil * @sumOil);
SET @aOilCF = (@sumCF - @bOilCF * @sumOil) / @n;

-- Регрессия Ткани на Капролактам
SET @sumCaprolactamCF = (SELECT SUM([Caprolactam] * [CF]) FROM [Asia_Table]);
SET @bCaprolactamCF = (@n * @sumCaprolactamCF - @sumCaprolactam * @sumCF) / (@n * @sumCaprolactam2 - @sumCaprolactam * @sumCaprolactam);
SET @aCaprolactamCF = (@sumCF - @bCaprolactamCF * @sumCaprolactam) / @n;

-- Регрессия Ткани на Полиамид-6
SET @sumPolyamidCF = (SELECT SUM([Polyamid] * [CF]) FROM [Asia_Table]);
SET @bPolyamidCF = (@n * @sumPolyamidCF - @sumPolyamid * @sumCF) / (@n * @sumPolyamid2 - @sumPolyamid * @sumPolyamid);
SET @aPolyamidCF = (@sumCF - @bPolyamidCF * @sumPolyamid) / @n;

-- Регресия Ткани на Нить
SET @sumFPTCF = (SELECT SUM([FPT] * [CF]) FROM [Asia_Table]);
SET @bFPTCF = (@n * @sumFPTCF - @sumFPT * @sumCF) / (@n * @sumFPT2 - @sumFPT * @sumFPT);
SET @aFPTCF = (@sumCF - @bFPTCF * @sumFPT) / @n;

UPDATE Forecast_Table SET [ForecastBenzeneAsia] = @bOilBenzene * [OilPrise] + @aOilBenzene;
UPDATE Forecast_Table SET [ForecastCaprolactamAsia] = @bOilCaprolactam * [OilPrise] + @aOilCaprolactam;
UPDATE Forecast_Table SET [ForecastPolyamidAsia] = @bOilPolyamid * [OilPrise] + @aOilPolyamid;
UPDATE Forecast_Table SET [ForecastFPTAsia] = ((@bOilFPT * [OilPrise] + @aOilFPT) + (@bCaprolactamFPT * [ForecastCaprolactamAsia] + @aCaprolactamFPT) + (@bPolyamidFPT * [ForecastPolyamidAsia] + @aPolyamidFPT)) / 3;
UPDATE Forecast_Table SET [ForecastCFAsia] = ((@bOilCF * [OilPrise] + @aOilCF) + (@bCaprolactamCF * [ForecastCaprolactamAsia] + @aCaprolactamCF) + (@bPolyamidCF * [ForecastPolyamidAsia] + @aPolyamidCF) + (@bFPTCF * [ForecastFPTAsia] + @aFPTCF)) / 4;
SELECT [ForecastBenzeneAsia], [ForecastCaprolactamAsia],  [ForecastPolyamidAsia], [ForecastFPTAsia], [ForecastCFAsia] FROM [Forecast_Table];