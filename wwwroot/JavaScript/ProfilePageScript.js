function ChangeFollowButton(Value) {
    var button = document.getElementById("Follow-Button");
    if (button)
    {
        button.innerText = Value;
    }
}
function GenerateBIOForm(BIOContent,UserId) {
    var BIOContainer = document.getElementById("UserBio-Conatiner");
    var FormContainer = document.createElement("div");
    BIOContainer.appendChild(FormContainer);
    var ChangeBioButton = document.getElementById("ChangeBioButtonIcon");
    ChangeBioButton.hidden = true;
    var BioParagarph = document.getElementById("UserBio");
    BioParagarph.hidden = true;
    var InputBio = document.createElement("textarea");
    InputBio.type = "text";
    InputBio.value = BIOContent;
    InputBio.id = "InputBio";
    InputBio.maxLength = 200;
    var Button = document.createElement("button");
    Button.id = "ChangeBioButton";
    Button.onclick = function () {
        var BioInput = document.getElementById("textarea");
        if (BioInput != null) {
            var BioContent = BioInput.value;
            var BioParagarph = document.getElementById("UserBio");
            BioParagarph.textContent = BioContent;
            fetch(`/Bio/ChangeBio?Bio=${BioContent}&UserId=${UserId}`, {
                method: "POST"
            });
            RemoveInupt();
        }
    }
    Button.textContent = "Change Bio";
    FormContainer.append(InputBio);
    FormContainer.append(Button);
}

function RemoveInupt()
{
    var InputBio = document.getElementById("InputBio");
    InputBio.remove();
    var BioParagarph = document.getElementById("UserBio");
    BioParagarph.hidden = false;
    var ChangeBioButtonIcon = document.getElementById("ChangeBioButtonIcon");
    ChangeBioButtonIcon.hidden = false;
    var ChangeBioButton = document.getElementById("ChangeBioButton");
    ChangeBioButton.remove();
}