document.getElementById('login-tab').addEventListener('click', function () {
    this.classList.add('active');
    document.getElementById('signup-tab').classList.remove('active');
    document.getElementById('login-form').style.display = 'flex';
    document.getElementById('signup-form').style.display = 'none';
});

document.getElementById('signup-tab').addEventListener('click', function () {
    this.classList.add('active');
    document.getElementById('login-tab').classList.remove('active');
    document.getElementById('signup-form').style.display = 'flex';
    document.getElementById('login-form').style.display = 'none';
});

document.getElementById('show-signup').addEventListener('click', function (e) {
    e.preventDefault();
    document.getElementById('signup-tab').click();
});

document.getElementById('show-login').addEventListener('click', function (e) {
    e.preventDefault();
    document.getElementById('login-tab').click();
});

ClientScript.RegisterStartupScript(this.GetType(), "DisableBackButton", "window.history.forward();", true);
