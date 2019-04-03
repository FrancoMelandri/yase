import React from 'react';


class Redirect extends React.Component {
    render() {
      const { params } = this.props.match
      return (
        <div>
          <h1>Redirect</h1>
          <p>{params.id}</p>
        </div>
      )
    }
  }
  export default Redirect;