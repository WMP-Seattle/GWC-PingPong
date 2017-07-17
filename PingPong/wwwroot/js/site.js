// Write your Javascript code.
    
    //The Root url, all api calls will extend this url.
    var rooturl = "http://localhost:5000/";

    //When the element with ID='submitScore' is clicked.
    $("#btn").click(function () {
        console.log('Button Clicked');
        sendPostRequest();
    })

    //Once an HTML Dom has been loaded, the .ready function will be called.
    //This function will update the leaderboard once the html has been loaded.
    $( "#elementToWatch" ).ready(function() {
        console.log( "Element ready!" );
    });

    //POST : posts data via the web api.
    function sendPostRequest() {
        //xhr -> XMLHttpRequest calls our web api.
        //This call is defaulted to the POST within the GamesController
        var xhr = {};
        xhr.type = "POST";          
        xhr.url = rooturl + "api/games";

        //Build out the data to be sent to the api call.
        xhr.contentType = "application/json";
        xhr.dataType = "html";
        xhr.data = {
            code: 100,
            message: "This is data I want to send",
            flag: true
        };

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

    //String formating function. Used to capitalize the first letter of each word in a string. 
    //  foo bar -> Foo Bar
    function toTitleCase(str)
    {
        return str.replace(/\w\S*/g, function(txt){return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();});
    }