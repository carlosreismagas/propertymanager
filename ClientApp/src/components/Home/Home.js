import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;
  constructor(props) {
    super(props);
    this.state = { properties: [], loading: true };
  }

  componentDidMount() {
    this.getProperties();
  }

  async getProperties() {
    const response = await fetch('api/property');
    const data = await response.json();
    this.setState({ properties: data, loading: false });
  }

  static renderProperties(properties) {
    return (
      <div>
        {properties.map(p =>
          <div>
            <div className="card">
              <div className="card-body">
                <h5 className="card-title">{p.description}</h5>
                <p className="card-text">{p.address}</p>
                <a href="#" className="btn btn-primary">Detalhes</a>
              </div>
            </div>
            <br></br>
          </div>
        )}
      </div>
    );
  }

  render() {
    let contents = this.state.loading ? <p>Loading.... </p> : Home.renderProperties(this.state.properties)
    return (
      <div>
        {contents}
      </div>
    );
  }
}
