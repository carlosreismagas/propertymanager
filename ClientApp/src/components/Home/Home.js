import React, { useState } from 'react';

export class Home extends React.Component {
  constructor(props) {
    super();
    var session = JSON.parse(localStorage.getItem("user_session"));
    this.state = {
      userr: { name: session.userObj.name, token: session.userObj.token }
    }
  }
  render () {
    return (
      <div>
        <p>Bem vindo <strong>{this.state.userr.name}</strong></p>
      </div>
    );
  }
}