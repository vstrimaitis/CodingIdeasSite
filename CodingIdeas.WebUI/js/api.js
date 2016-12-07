//var localStorage.getItem("UserId") = localStorage.getItem("UserId");//'c922148e-a8a4-4c02-bd0e-0cd31946b29b';
var URL = 'http://localhost/CodingIdeas/api/';

function loadPosts()
{
    var pageNumber = window.location.hash.substr(1);
    if(pageNumber <= 0)
        return;
    if(pageNumber == 1)
        $('#backButton').addClass('disabled');
    else
        $('#backButton').removeClass('disabled');
    $.getJSON('http://localhost/CodingIdeas/api/Post/InPage/'+pageNumber, function(data){
        var posts = [];
        data.forEach(function(item){
            posts.push(item);
        });
        return posts;
    }).then(function(data){
        if(data.length == 0){
            $('#forwardButton').addClass('disabled');
            window.location.hash = '#' + (pageNumber-1);
            return;
        }
        else
            $('#forwardButton').removeClass('disabled');
        $('#posts').html("");
        data.forEach(function(item){
            $('#posts').append(buildPostPreview(item));
        });
    });
}

function addComment(postId, authorId, content)
{
    var comment = {
        AuthorId: authorId,
        PublishDate: new Date().toISOString(),
        Content: content,
        PostId: postId
    };
    $.post(URL + 'Comment', comment, function(data, status, xhr){
        if(status == "success")
            loadComments(postId, 1);
    });
}

function loadPost(id)
{
    $.getJSON(URL + 'Post/'+id, function(data){
        $('#post').html(buildPost(data));
    });
}

function loadComments(postId, pageNumber)
{
    $.getJSON(URL + 'Comment/'+postId + '/' + pageNumber, function(data){
        if(data.length == 0)
            return;
        $('#comments').html("");
        data.forEach(function(item){
           $('#comments').append(buildComment(item));
        });
    });
}

function loadTopPosters()
{
    $.getJSON(URL + 'User/MostActivePosters/10', function(data){
        $('#topPostersTable').html("<tr><th>Username</th><th>Number of posts</th></tr>");
        data.forEach(function(item){
            var rowContents = '<td><a href="user.html#'+item.UserId+'">'+item.Username+'</a></td>' +
                                '<td>'+item.NumberOfPosts+'</td>';
            var row = '<tr>'+rowContents+'</tr>';
            $('#topPostersTable').append(row);
        });
    });
}