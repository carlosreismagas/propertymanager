import React, { Component } from 'react';

export class Login extends Component {
    render() {
        return (
            <div>
                {contents}
            </div>
        );
    }

    Login() {
        const post = {
            Username: document.getElementById("Username"),
            Password: document.getElementById("Password")
        };

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(post)
        };

        fetch('api/user/login', requestOptions)
            .then(response => response.json())
            .then(data => {
                if (data) {

                }
            });
    }
}
