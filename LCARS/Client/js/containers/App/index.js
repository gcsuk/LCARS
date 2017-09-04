import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { connect } from 'react-redux';
import { mapStateToProps, mapDispatchToProps } from './state';
import '../../../css/index.css';
import Layout from '../../components/Layout';
import HomePage from '../../components/HomePage';
import BuildsPage from '../../components/BuildsPage';

class App extends Component {
  render() {
    return (
      <Layout children={this.props.children} />
    );
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(App);