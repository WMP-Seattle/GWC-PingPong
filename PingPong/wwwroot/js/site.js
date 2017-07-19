//GWC-PingPong: The file will be required to change the functionally of the frontend.
//              Code within this file can change html elements directly.
//              Send API calls to the backend.

//TODO: Create functions which will submit the html form for adding a completed game.
//          POST api call to send data.
//TODO: Update the Leaderboard - on page load as well as when a new game is added.
//          GET api call to retrieve list of players.
//          Generate an html table to show list of players.
    
    //The Root url, all api calls will extend this url.
    var rooturl = "http://localhost:5000/";

    //Use this function to run code when an element with Id='btn' is clicked. 
    $("#btn").click(function () {
        console.log('Button Clicked');
        sendPostRequest();
    })

    //Once an HTML Dom has been loaded, the .ready function will be called.
    //This function will run once the html for the element with id='title' has been loaded.
    $( "#title" ).ready(function() {
        console.log( "Title ready!" );
    });

    //POST : posts data via the web api.
    //Use these call to send data to the back end, to either have logic run on it or to have it saved into a database.
    function sendPostRequest() {
        //xhr -> XMLHttpRequest calls our web api.
        //This call is defaulted to the POST within the GamesController
        var xhr = {};
        xhr.type = "POST";          
        xhr.url = rooturl + "api/games";

        //Build out the data to be sent to the api call.
        xhr.contentType = "application/json";
        xhr.dataType = "html";
        var data = {
            code: 100,
            message: "This is data I want to send",
            flag: true
        };

        //Data must be sent via JSON format.
        xhr.data = JSON.stringify(data);

        //.success is the callback function that will be called if the api call succeeds.
        xhr.success = function (result, status, xhr) {
            alert(status);
        };
        //.error is the callback function that will be called if the api call fails.
        xhr.error = function (xhr, status, statusText) {
            alert(xhr.status + " " + xhr.statusText);
        };
        $.ajax(xhr);
    }

    //GET request to the web api.
    //Use the function to retreieve information from the backend, via a database.
    function sendGetRequest() {
        var xhr = {
            type: "GET",
            url: rooturl + "api/players/0"
        };
        xhr.success = function (result, status, xhr) {
            alert(status + "\n" + result);
        };
        xhr.error = function (xhr, status, statusText) {
            alert(status + " " + statusText);
        };
        $.ajax(xhr);
    }

    //This function will be used in order to build out the html table required to display the Leaderboard.
    //Input: a list of pre-sorted players.
    function buildLeaderboard(players) {
       //TODO: Implement code. 
       //The table thead (the column headers) can be generated within this funciton or staticly created within the html.
       //Loop through the list of players and append rows of data to an html tbody object to display that data.
    }

    //String formating function. Used to capitalize the first letter of each word in a string. 
    //  foo bar -> Foo Bar
    function toTitleCase(str)
    {
        return str.replace(/\w\S*/g, function(txt){return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();});
    }