var MyApp = angular.module('MyApp', ['ngSanitize','ngRoute']);
MyApp
    .config([
        '$httpProvider', function ($httpProvider) {
            $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
        }
    ])
    .config([
        '$compileProvider', function ($compileProvider) {
            $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|javascript):/);
        }
    ]);