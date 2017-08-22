import React, {PropTypes} from 'react';
import ReactDOM from 'react-dom';
//import '../css/index.css';
import Header from './components/Header';
import HomePage from './components/HomePage';
import BuildsPage from './components/BuildsPage';

class App extends React.Component {
  render() {
    return (
      <div>
        {this.props.children}
      </div>
    );
  }
}

App.propType = {
  children: PropTypes.object.isRequired
};

export default App;
