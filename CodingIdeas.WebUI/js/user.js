$(document).ready(function ()
{
    loadUserInfo();

});

function loadUserInfo()
{
    var userId = window.location.hash.substr(1);
    $.getJSON(URL + 'User/'+userId, function(data){
        if(data.length == 0)
            return;
        $('#userInfoContainer').html(buildUserPage(data));
    });
}

function buildUserPage(user)
{
    var fullPage = "";
    var titlePart = '<h1>Welcome to '+user.Username+'\'s profile!</h1>';
    var infoStartPart = '<h3>Here\'s some info:</h3>';
    var name = 'Not given :(';
    if(user.FirstName != null && user.LastName != null)
        name = user.FirstName + " " + user.LastName;
    else if(user.FirstName != null)
        name = user.FirstName + " ???";
    else if(user.LastName != null)
        name = "??? " + user.LastName;
    var nameRow = '<tr><td>Full name:</td><td>'+name+'</td></tr>';
    var emailRow = '<tr><td>Email:</td><td>'+user.Email+'</td></tr>';
    var dob = 'Not given :(';
    if(user.DateOfBirth != null)
        dob = new Date(user.DateOfBirth).toLocaleDateString();
    var dobRow = '<tr><td>Date of birth:</td><td>'+dob+'</td></tr>';
    var infoTable = '<table class="table table-hover" id="personalInfoTable">'+
                    nameRow + 
                    emailRow + 
                    dobRow +
                    '</table>';
    
    var skillsStartPart = '<h3>'+user.Username+'\'s mad skills:</h3>';
    var allRows = "";
    user.Skills.forEach(function(item){
       var row = '<tr><td>'+item.ProgrammingLanguage.Name+'</td><td>'+item.Proficiency+'</td></tr>'; 
        allRows += row;
    });
    
    var skillsTable = '<table class="table table-hover" id="skillsTable"><tr><th>Programming language</th><th>Proficiency level</th></tr>' + 
                        allRows +
                        '</table>';
    
    fullPage = titlePart + infoStartPart + infoTable + skillsStartPart + skillsTable;
    return fullPage;
}