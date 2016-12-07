var programmingLangs = [];
var alreadySelectedLangs = [];
$(document).ready(function ()
{
    loadProgrammingLanguages();
    loadUserInfo();
});
$(document).on('click', '.removeSkillRow', function(){
    var id = $(this).closest('tr').find('td > input.langId')[0].value;
    removeLang(id);
    $(this).closest('tr').remove();
});
$(document).on('click', '#newSkillButton', function(){
    $('#skillsTable tr:last').html(buildNewLastRow());
    var row = '<tr><td>'+makeLanguagesComboBox(null)+'</td><td><input class="proficiencySlider" type="range" min="1" max="5" name="proficiency"/></td><td>'+makeButtons()+'</td></tr>';
    $('#skillsTable').append(row);
});

function buildNewLastRow()
{
    var lastSelect = $('#skillsTable tr:last > td > select');
    var lastRange = $('#skillsTable tr:last > td > input.proficiencySlider');
    if(lastSelect[0] == null)
        return $('#skillsTable tr:last').html();
    var langId = lastSelect[0].value;
    var langName = lastSelect[0].options[lastSelect[0].selectedIndex].text;
    var lang = {
        Id: langId,
        Name: langName
    };
    alreadySelectedLangs.push(lang);
    var cont1 = '<input type="hidden" class="langId" value="' + langId + '"><span class="langName">' + langName + '</span>';
    var contents = '<td>'+cont1+'</td><td>' + lastRange[0].value + '</td><td>'+makeButtons()+'</td>';
    return contents;
}

function makeLanguagesComboBox(toSelect)
{
    var allOptions = "";
    programmingLangs.forEach(function(item){
        if(!contains(item)){
            var selected = '';
            if(item.Id === toSelect)
                selected = 'selected="true"';
            var opt = '<option value="'+item.Id+'" '+selected+'>'+item.Name+'</option>';
            allOptions += opt; 
        }
    });
    return '<select>'+allOptions+'</select>';
}

function removeLang(id)
{
    var ind;
    for(var i = 0; i < alreadySelectedLangs.length; i++)
        if(alreadySelectedLangs[i].Id === id){
            ind = i;
            break;
        }
    alreadySelectedLangs.splice(ind, 1);
}

function contains(lang)
{
    for(var i = 0; i < alreadySelectedLangs.length; i++)
        if(alreadySelectedLangs[i].Id === lang.Id)
           return true;
    return false;
}

function loadProgrammingLanguages()
{
    $.getJSON(URL + 'ProgrammingLanguage', function(data){
       data.forEach(function(item){
           programmingLangs.push(item);
       });
    });
}

function loadUserInfo()
{
    var userId = localStorage.getItem("UserId");
    $.getJSON(URL + 'User/'+userId, function(data){
        if(data.length == 0)
            return;
        $('#userInfoContainer').html(buildUserPage(data));
    });
}

function saveChanges()
{
    $('#skillsTable tr:last').html(buildNewLastRow());
    var fName = document.getElementById('firstNameBox').value;
    var lName = document.getElementById('lastNameBox').value;
    var email = document.getElementById('emailBox').value;
    var dob = document.getElementById('dobBox').value;
    var skills = [];
    var table = document.getElementById('skillsTable');
    for(var i = 1; i < table.rows.length; i++){
        var row = table.rows[i]
        var langCell = row.cells[0];
        var profCell = row.cells[1];
        
        var langId      = $(langCell).find('input')[0].value;
        var langName    = $(langCell).find('span')[0].innerHTML;
        var proficiency = profCell.innerHTML;
        
        //alert(langId+" "+langName+" "+proficiency);
        
        var lang = {
            Id: langId,
            Name: langName
        };
        var skill = {
            ProgrammingLanguage: lang,
            Proficiency: proficiency
        };
        skills.push(skill);
    }
    var user = {
        Id: localStorage.getItem("UserId"),
        FirstName: fName,
        LastName: lName,
        Email: email,
        DateOfBirth: dob,
        Skills: skills
    }
    
    $.ajax({
        url: URL + 'User',
        type: 'PUT',
        data: user,
        success: function(response){
            alert('Saved successfully');
        }
    });
}

function buildUserPage(user)
{
    var fullPage = "";
    var titlePart = '<h1>Welcome to '+user.Username+'\'s profile!</h1>';
    var infoStartPart = '<h3>Here\'s your info:</h3>';
    var firstNameRow = '<tr><td>First name:</td><td><input type="text" id="firstNameBox" placeholder="First name" value="'+user.FirstName+'" /></td></tr>';
    var lastNameRow = '<tr><td>Last name:</td><td><input type="text" id="lastNameBox" placeholder="Last name" value="'+user.LastName+'" /></td></tr>';
    var emailRow = '<tr><td>Email:</td><td><input type="email" id="emailBox" placeholer="your@email.com" value="'+user.Email+'" /></td></tr>';
    
    var dobRow = '<tr><td>Date of birth:</td><td><input type="date" id="dobBox" value="'+new Date(user.DateOfBirth).toLocaleDateString()+'" /></td></tr>';
    var infoTable = '<table class="table table-hover" id="personalInfoTable">'+
                    firstNameRow + 
                    lastNameRow +
                    emailRow + 
                    dobRow +
                    '</table>';
    
    var skillsStartPart = '<h3>And here are your mad skills:</h3>';
    var allRows = "";
    user.Skills.forEach(function(item){
        var row = '<tr><td><input type="hidden" class="langId" value="'+item.ProgrammingLanguage.Id+'"><span class="langName">'+item.ProgrammingLanguage.Name+'</span></td><td>'+item.Proficiency+'</td><td>'+makeButtons()+'</td></tr>'; 
        alreadySelectedLangs.push(item.ProgrammingLanguage);
        allRows += row;
    });
    
    var skillsTable = '<table class="table table-hover" id="skillsTable"><tr><th>Programming language</th><th>Proficiency level</th><th></th></tr>' + 
                        allRows +
                        '</table>';
    var newSkillButton = '<table class="table table-hover" id="skillsTable"><tr><th class="unselectable" id="newSkillButton">+</th></tr></table>';
    var submitButton = '<button type="submit" class="btn btn-primary">Save</button>';
    
    fullPage = '<form onsubmit="saveChanges(); return false">' + titlePart + infoStartPart + infoTable + skillsStartPart + skillsTable + newSkillButton + submitButton + '</form>';
    return fullPage;
}

function makeButtons()
{
    return '<a class="removeSkillRow unselectable">Remove</a>';
}