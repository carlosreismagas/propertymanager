import React, { useState } from 'react';
import { Link, NavLink } from 'react-router-dom';

export class ListProperty extends React.Component {       
  constructor(props) {
    super(props)
    this.state = {
      properties: [],
      loading: true
    }

    fetch('api/property').then(res => res.json())
    .then(data => {
      this.setState({properties: data, loading: false });
    });
  }
  
  render() {
    const renderProperties = (prop) => {
      return (
        <div>
          {prop.map(p =>
            <div key={p.id}>
              <div className="card">
                <div className="card-body">
                  <h5 className="card-title">{p.description}</h5>
                  <p className="card-text">{p.address}</p>
                  <NavLink className="btn btn-primary" tag={Link} to={"/property/" + p.id}>Detalhes</NavLink>
                </div>
              </div>
              <br></br>
            </div>
          )}
        </div>
      );
    }

    let contents = this.state.loading ? <p>Loading.... </p> : renderProperties(this.state.properties)
    return (
      <div>
        {contents}
      </div>
    );
  }
}