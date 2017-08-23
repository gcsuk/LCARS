import React, {PropTypes} from 'react';
import ReactDOM from 'react-dom';
import '../css/index.css';
import Layout from './components/Layout';
import HomePage from './components/HomePage';
import BuildsPage from './components/BuildsPage';

class App extends React.Component {
  render() {
    return (
      <div>
        <Layout children={this.props.children} />
      </div>
    );
  }
}

App.propType = {
  children: PropTypes.object.isRequired
};

export default App;
