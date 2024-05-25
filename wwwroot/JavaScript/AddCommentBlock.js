function AddCommentBlock(ElementId) {
    var PostDiv = document.getElementById(ElementId);
    var NumberOfChilds = PostDiv.childElementCount;
    if (NumberOfChilds == 3)
    {
        var newDiv = document.createElement('div');
        newDiv.classList.add('Comment-Block');
        PostDiv.appendChild(newDiv);
    }
   
}