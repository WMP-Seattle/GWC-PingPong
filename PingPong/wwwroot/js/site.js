// Write your Javascript code.
    
    //The Root url, all api calls will extend this url.
    var rooturl = "http://localhost:5000/";

    //When the element with ID='submitScore' is clicked.
    $("#submitScore").click(function () {
        console.log('Submit Clicked');
        submitGame();
    })

    //Saves a new game into the database via the api/games
    function submitGame() {
        //options is a REST object, to call our web api.
        var xhr = {};
        xhr.url = rooturl + "api/games";
        xhr.type = "POST";          

        //Build out the data to be sent to the api call.

        //Player objects. PingPong/Models/Players.cs
        var playerOneObj = {
            Name : $("#Player1Name").val()
        };            
        var playerTwoObj = {
            Name : $("#Player2Name").val()
        };

        //Game object. PingPong/Models/Games.cs
        var gameObj = {
            PlayerOne : playerOneObj, 
            PlayerOneScore: $("#Player1Score").val(), 
            PlayerTwo : playerTwoObj, 
            PlayerTwoScore : $("#Player2Score").val(), 
        };

        xhr.data = JSON.stringify(gameObj);

        xhr.contentType = "application/json";
        xhr.dataType = "html";

        console.log('options: ' + JSON.stringify(xhr));

        //.success is the callback function that will be called if the api call succeeds.
        xhr.success = function (result, status, xhr) {
            alert(status);
            updateLeaderboard();
        };
        //.error is the callback function that will be called if the api call fails.
        xhr.error = function (xhr, status, statusText) {
            alert(xhr.status + " " + xhr.statusText);
        };
        $.ajax(xhr);
    }

    //Once an HTML Dom has been loaded, the .ready function will be called.
    //This function will update the leaderboard once the html has been loaded.
    $( "#leaderboard" ).ready(function() {
        console.log( "ready!" );
        updateLeaderboard();
    });

    //Function to build out the HTML table to show the leaderboard.
    function buildLeaderboard(players) {
        //Remove the old table body.
        $('#leaderboard tbody').empty();

        var table = document.getElementById('leaderboard');
        var tableBody = table.getElementsByTagName('tbody').item(0);
        //var tableBody = document.createElement('tbody');

        //Loop through the player object, add the data to each cell via the generateRow func.
        players.forEach(function(player, index) {
            var row = generateRow(player, index);
            tableBody.appendChild(row);
        });

        table.appendChild(tableBody);
    }

    //Function to place relevent cell data into a row and return that row.
    function generateRow(player, index) {
        var row = document.createElement('tr');

        var rank = document.createElement('td');
        rank.appendChild(document.createTextNode(index+1+'.'));
        row.appendChild(rank);
        
        var name = document.createElement('td');
        name.appendChild(document.createTextNode(toTitleCase(player.name)));
        name.className += "text-center ";
        row.appendChild(name);
        
        var wins = document.createElement('td');
        wins.appendChild(document.createTextNode(player.numberWins));
        wins.className += "text-center ";
        row.appendChild(wins);
        
        var losses = document.createElement('td');
        losses.appendChild(document.createTextNode(player.numberLosses));
        losses.className += "text-center ";
        row.appendChild(losses);

        return row
    }

    //api call to fetch leaderboard.
    function updateLeaderboard() {
        var xhr = {
            type: "GET",
            url: rooturl + "api/players/leaderboard"
        };
        xhr.success = function (result, status, xhr) {
            console.log("Leaderboard:", result);
            buildLeaderboard(result);
        };
        xhr.error = function (xhr, status, statusText) {
            alert(xhr.status + " " + xhr.statusText);
        };
        $.ajax(xhr);
    }
    
    //String formating function. Used to capitalize the first letter of each word in a string. 
    //  foo bar -> Foo Bar
    function toTitleCase(str)
    {
        return str.replace(/\w\S*/g, function(txt){return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();});
    }