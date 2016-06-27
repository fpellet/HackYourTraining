(function() {

    var app = angular.module("hackYourTraining", []);

    app.controller("hackYourTrainingController", function($scope, $http) {
        var self = this;

        self.interestedTrainees = [];
        self.nbInterestedTrainees = 0;

        var load = function() {
            $http.get('/interestedTrainees').success(function(data) {
                self.interestedTrainees = data.interestedTrainees;
                self.nbInterestedTrainees = self.interestedTrainees.length; 
            });
        };

        load();
    });
})();