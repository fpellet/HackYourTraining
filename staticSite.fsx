let html = """
    <html>
        <head>
            <title>Hack your training</title>
            <link href="/static/style.css" type="text/css" rel="stylesheet">
            <link href='//fonts.googleapis.com/css?family=Source+Sans+Pro:400,700' rel='stylesheet' type='text/css'>
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
        </head>
        <body ng-app="hackYourTraining" ng-cloak>    
            <div ng-controller="hackYourTrainingController as hack" id="container">
                
                <h1>Welcome to Hack Your Training</h1>
                <br>
                <h2>The Lyon's community would like to be trained on CQRS/ES by <a href="https://twitter.com/gregyoung" target="_blank">Greg Young</a> in september 2016</h1>
                <br>
                <h3>Currently interested people (3/15)</h3>
                <ul>
                    <li ng-repeat="trainee in hack.interestedTrainees">
                         <a ng-href="{{trainee.twitterUrl}}">{{trainee.name}}</a>                          
                    </li>
                </ul>
            </div>
            <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.3.15/angular.min.js"></script>
            <script src="/static/app.js"></script>
        </body>
    </html>
"""

let script = """
    (function() {
        
        var app = angular.module("hackYourTraining", []);
        
        app.controller("hackYourTrainingController", function($scope, $http) {    
            var self = this;
            
            self.text = "";
            self.interestedTrainees = [];
           
            var load = function() { 
                $http.get('/interestedTrainees').success(function(data) {
                    self.interestedTrainees = data.interestedTrainees;
                }); 
            };
                        
            load();
        });
    })();
"""

let style = """
    html, body { margin: 0; padding: 0; width: 100%; height: 100%; }
    #container { margin: 50px; font-family: "Source Sans Pro", sans-serif; font-weight: 400; }
        h1 { font-weight: 700; }
        a { text-decoration: none; }
        li { margin: 20px 0 0 0; }
        input { padding: 10px; }
"""