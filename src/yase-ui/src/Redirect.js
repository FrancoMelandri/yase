import React from 'react';
const axios = require('axios')
const endpoint = require('./config').default


class Redirect extends React.Component {
    render() {
      const { params } = this.props.match

      axios.post(endpoint.URL + '/engine', {
        Url: 'http://base.com/' + params.id
      }).then(response => {
        window.location = response.data.originalUrl
      }).catch (error => { alert(error)})
      return (
        <div>
        </div>
      )
    }
  }
  export default Redirect;