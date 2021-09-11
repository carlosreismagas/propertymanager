import React, { useState } from 'react';
import './Login.css';
const Login = () => {
    const [register, setRegister] = useState(false);
    const [post, setPost] = useState({
        Email: "",
        Name: "",
        Username: "",
        Password: ""
    });

    var timer = setInterval(function () {
        if (document.getElementById("Username")) {
            document.querySelectorAll("input").forEach(item => {
                item.setAttribute("autocomplete", "off");
            });
            clearTimeout(timer);
        }
    });

    const sign = (e) => {
        e.preventDefault();
        var url = register ? "api/user/register" : "api/user/login"

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(post)
        }

        fetch(url, requestOptions)
            .then(response => response.json())
            .then(data => {
                if (data) {
                    var session = {
                        userObj: data.user,
                        token: data.token
                    }

                    localStorage.setItem("user_session", JSON.stringify(session));
                    window.location.href = "/";
                }
            }).catch(err => { console.log(err) });
    }

    const toggleRegister = () => {
        setRegister(!register);
    }

    if (!register) {
        return (
            <div className="login">
                <h4 style={{ textAlign: "center" }}>Wealth Manager</h4>
                <form onSubmit={sign}>
                    <label>Username</label>
                    <input id="Username" type="text" className="form-control" value={post.Username} onChange={(e) => setPost({ ...post, Username: e.target.value })} />

                    <label>Password</label>
                    <input id="Password" type="password" className="form-control" value={post.Password} onChange={(e) => setPost({ ...post, Password: e.target.value })} />
                    <br></br>
                    <button type="submit" className="btn btn-block">Login</button>
                </form>
                <p style={{ textAlign: "center" }}>Don't have an account yet? <span onClick={toggleRegister}>Sign Up</span></p>
            </div>
        );
    } else {
        return (
            <div className="login">
                <h4 style={{ textAlign: "center" }}>Wealth Manager</h4>
                <form onSubmit={sign}>
                    <label>Email</label>
                    <input id="Email" type="text" className="form-control" value={post.Email} onChange={(e) => setPost({ ...post, Email: e.target.value })} />

                    <label>Name</label>
                    <input id="Name" type="text" className="form-control" value={post.Name} onChange={(e) => setPost({ ...post, Name: e.target.value })} />

                    <label>Username</label>
                    <input id="Username" type="text" className="form-control" value={post.Username} onChange={(e) => setPost({ ...post, Username: e.target.value })} />

                    <label>Password</label>
                    <input id="Password" type="password" className="form-control" value={post.Password} onChange={(e) => setPost({ ...post, Password: e.target.value })} />
                    <br></br>
                    <button type="submit" className="btn btn-block">Register</button>
                </form>
                <p style={{ textAlign: "center" }}>Already have an account yet? <span onClick={toggleRegister}>Login</span></p>
            </div>
        );
    }
}

export default Login;