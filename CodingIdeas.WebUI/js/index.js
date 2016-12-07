$(document).ready(function ()
{
    if(localStorage.getItem("UserId") === null){
        $('.loggedInLinks').remove();
    }else{
        $('.loggedOutLinks').remove();
    }
    if(window.location.hash == "")
        window.location.hash = "#1";
    $('.navigationButtons').click(function(e){e.preventDefault(); return false;});
    $('.navigationButtons').click(function (e) {
        e.preventDefault();
        if ($(this).hasClass('disabled'))
            return false; // Do something else in here if required
    });
    loadTopPosters();
    loadPosts();

});

function buildPostPreview(post)
{
    var titleLink = '<a href="post.html#'+post.Id+'">'+post.Title+'</a>';
    var deleteButtonPart = '';
    if(post.AuthorId == localStorage.getItem("UserId"))
        deleteButtonPart = '<button style="margin-left: 15px" class="btn btn-danger btn-xs" onclick="removePostClick(\''+post.Id+'\')" ><span class="glyphicon glyphicon-trash"></span></button>'
    var titlePart = '<h2>'+titleLink + deleteButtonPart +'</h2>';
    

    var authorPart = '<p class="lead">'+
                     'by <a href="user.html#'+post.AuthorId+'">'+post.AuthorUsername+'</a>'+
                     '</p>';
    var publishDatePart = '<p><span class="glyphicon glyphicon-time"></span> Posted on '+new Date(post.PublishDate).toDateString()+'</p>';
    var line = '<hr />';
    var contentPart = '<p>'+post.Content+'</p>';
    var readMorePart = '<a class="btn btn-primary" href="post.html#'+post.Id+'">Read More <span class="glyphicon glyphicon-chevron-right"></span></a>';
    var full = '<div class="postPreview">' + 
                titlePart + 
                authorPart + 
                publishDatePart + 
                line + 
                contentPart + 
                readMorePart + 
                line + 
                '</div>';
    return full;
}

function removePostClick(id){
    $.ajax({
        url: URL + 'Post/'+id,
        type: 'DELETE',
        success: function(response){
            alert("Successfully deleted post!");
            loadPosts();
            loadTopPosters();
        }
    });
}

function goBack()
{
    var currPage = window.location.hash.substr(1);
    var newPage = currPage - 1;
    if(newPage <= 0)
        return;
    window.location.hash = '#' + newPage;
    loadPosts();
}

function goForward()
{
    var currPage = window.location.hash.substr(1);
    var newPage = (currPage - 0) + 1;
    window.location.hash = '#' + newPage;
    loadPosts();
}



function logout()
{
    localStorage.removeItem('UserId');
    location.reload();
}


function tryLogin()
{
    var email = document.getElementById('emailBox').value;
    var password = document.getElementById('passwordBox').value;
    $.ajax({
       url:  URL + 'User/Validate/' + email + '/' + password,
        type: 'GET',
        success: function(response){
            localStorage.setItem("UserId", response);
            location.reload();
        },
        error: function(exception){
            alert("Login attempt failed");
        }
    });
}