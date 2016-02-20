(function () {
    'use strict';

    function weatherInfoFactory() {

        var icons = {
            iconNight: "icon-night",
            iconSunny: "icon-sunny",
            iconFrosty: "icon-frosty",
            iconWindySnow: "icon-windysnow",
            iconShowers: "icon-showers",
            iconBaseCloud: "icon-basecloud",
            iconCloud: "icon-cloud",
            iconRainy: "icon-rainy",
            iconMist: "icon-mist",
            iconWindySnowClout: "icon-windysnowcloud",
            iconDrizzle: "icon-drizzle",
            iconSnow: "icon-snowy",
            iconSleet: "icon-sleet",
            iconMoon: "icon-moon",
            iconWindyRain: "icon-windyrain",
            iconHail: "icon-hail",
            iconSunset: "icon-sunset",
            iconWindyRainCloud: "icon-windyraincloud",
            iconSunrise: "icon-sunrise",
            iconSun: "icon-sun",
            iconThunder: "icon-thunder",
            iconWindy: "icon-windy"
        }

        var titles = {
            thunderstorm: "burza",
            drizzle: "mrzawka",
            rain: "opady deszczu",
            snow: "opady śniegu",
            sun: "słońce",
            lightThunderstorm: "łagodna burza",
            lightDrizzle: "łagodna mrzawka",
            lightRain: "łagodne opady deszczu",
            lightSnow: "łagodne opady śniegu",
            heavyThunderstorm: "silna burza",
            heavyDrizzle: "silna mrzawka",
            heavyRain: "intensywny deszcz",
            heavySnow: "intensywne opady śniegu",
            clearSky: "czyste niebo",
            clouds: "średnie zachmurzenie",
            lightClouds: "lekkie zachmurzenie",
            heavyClouds: "silne zachmurzenie"
        }

        function getWeatherInfo(weatherData) {
            var weatherFullModel = [];

            console.log(weatherData);
            for (var i = 0; i < weatherData.length; i++) {
                var weatherModel = getWeatherModelsFromId(weatherData[i].WeatherId);
                for (var j = 0; j < weatherModel.length; j++) {
                    weatherFullModel.push(weatherModel[j]);
                }
            }
            return weatherFullModel;

        }

        function getModel(title, icons) {
            return { title, icons}
        }

        function getWeatherModelsFromId(weatherId) {
            var weatherModel = [];
            switch (weatherId) {
                //Thunderstorm 2xx codes
                case 200:
                    {
                        weatherModel.push(getModel(titles.thunderstorm, icons.iconThunder));
                        weatherModel.push(getModel(titles.lightRain, icons.iconRainy));
                        break;
                    }
                case 201:
                    {
                        weatherModel.push(getModel(titles.thunderstorm, icons.iconThunder));
                        weatherModel.push(getModel(titles.rain, icons.iconRainy));
                        break;
                    }
                case 202:
                    {
                        weatherModel.push(getModel(titles.thunderstorm, icons.iconThunder));
                        weatherModel.push(getModel(titles.heavyRain, icons.iconRainy));
                        break;
                    }
                case 210:
                    {
                        weatherModel.push(getModel(titles.lightThunderstorm, icons.iconThunder));
                        break;
                    }
                case 211:
                    {
                        weatherModel.push(getModel(titles.thunderstorm, icons.iconThunder));
                        break;
                    }
                case 212:
                case 221:
                    {
                        weatherModel.push(getModel(titles.heavyThunderstorm, icons.iconThunder));
                        break;
                    }
                case 230:
                    {

                        weatherModel.push(getModel(titles.thunderstorm, icons.iconThunder));
                        weatherModel.push(getModel(titles.lightDrizzle, icons.iconDrizzle));
                        break;
                    }
                case 231:
                    {

                        weatherModel.push(getModel(titles.thunderstorm, icons.iconThunder));
                        weatherModel.push(getModel(titles.drizzle, icons.iconDrizzle));
                        break;
                    }
                case 232:
                    {

                        weatherModel.push(getModel(titles.thunderstorm, icons.iconThunder));
                        weatherModel.push(getModel(titles.heavyDrizzle, icons.iconDrizzle));
                        break;
                    }

                    //Drizzle 3xx codes
                case 300:
                case 301:
                    {
                        weatherModel.push(getModel(titles.lightDrizzle, icons.iconDrizzle));
                        break;
                    }
                case 302:
                case 310:
                    {
                        weatherModel.push(getModel(titles.heavyDrizzle, icons.iconDrizzle));
                        break;
                    }
                case 311:
                    {
                        weatherModel.push(getModel(titles.rain, icons.iconDrizzle));
                        break;
                    }
                case 312:
                case 313:
                case 314:
                case 321:
                    {
                        weatherModel.push(getModel(titles.rain, [icons.iconRainy, icons.iconBaseCloud]));
                        break;
                    }

                case 500:
                case 501:
                case 520:
                case 521:
                    {
                        weatherModel.push(getModel(titles.rain, [icons.iconRainy, icons.iconBaseCloud]));
                        break;
                    }
                case 511:
                case 502:
                case 503:
                case 504:
                case 522:
                case 531:
                    {
                        weatherModel.push(getModel(titles.heavyRain, [icons.iconRainy, icons.iconBaseCloud]));
                        break;
                    }

                case 600:
                case 601:
                    {
                        weatherModel.push(getModel(titles.snow, icons.iconSnow));
                        break;
                    }
                case 602:
                    {
                        weatherModel.push(getModel(titles.heavySnow, icons.iconSnow));
                        break;
                    }
                case 611:
                case 612:
                case 615:
                case 616:
                case 620:
                case 621:
                case 622:
                    {

                        weatherModel.push(getModel(titles.rain, icons.iconRainy));
                        weatherModel.push(getModel(titles.snow, icons.iconSnow));
                        break;
                    }
                case 800:
                    {
                        weatherModel.push(getModel(titles.clearSky, icons.iconSunny));
                        break;
                    }
                case 801:
                    {
                        weatherModel.push(getModel(titles.lightClouds, icons.iconCloud));
                        break;
                    }
                case 802:
                    {
                        weatherModel.push(getModel(titles.lightClouds, icons.iconCloud));
                        break;
                    }
                case 803:
                    {
                        weatherModel.push(getModel(titles.clouds, icons.iconCloud));
                        break;
                    }
                case 804:
                    {
                        weatherModel.push(getModel(titles.heavyClouds, icons.iconCloud));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return weatherModel;
        }

        return {
            getWeatherInfo: getWeatherInfo
        }
    }


    angular.module('portalApp').factory('weatherInfoFactory', weatherInfoFactory);
})();