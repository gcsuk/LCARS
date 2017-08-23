import React from 'react';

class Digits extends React.Component {
  constructor(props) {
    super(props);
    this.state = {rowStatus: new Array(props.rowCount)};
  }

  componentDidMount() {
    this.timerID = setInterval(() => this.pickRows(this.props.rowCount), 1000);
  }

  componentWillUnmount() {
    clearInterval(this.timerID);
  }

  pickRows(rowCount) {
    var selectedRow = Math.floor(Math.random() * rowCount);

    var newState = JSON.parse(JSON.stringify(this.state)).rowStatus;

    for (var i = 0, len = newState.length; i < len; i++) {
      newState[i] = "";
    }

    newState[selectedRow] = "white";
    newState[selectedRow + 1] = "white";

    this.setState({ rowStatus: newState });
  }

  render() {
    return (
      <table>
          <tbody>
            <tr className={this.state.rowStatus[0]}><td>2385</td><td>8578232</td><td>9</td><td>5789</td><td>3882</td><td>5893</td><td>9865</td><td>3489</td><td>2485</td><td>0846</td><td>9798</td><td>9629</td><td>29</td></tr>
            <tr className={this.state.rowStatus[1]}><td>2064</td><td>2064962</td><td>7</td><td>9776</td><td>626</td><td>1276</td><td>7612</td><td>126</td><td>97</td><td>6165</td><td>6626</td><td>876</td><td>74</td></tr>
            <tr className={this.state.rowStatus[2]}><td>34</td><td>279</td><td></td><td>89</td><td>6580</td><td>6547</td><td>6587</td><td>3465</td><td>867</td><td>2347</td><td>5762</td><td>4588</td><td>05</td></tr>
            <tr className={this.state.rowStatus[3]}><td>4768</td><td>8967248</td><td>7</td><td>9798</td><td>8969</td><td>476</td><td>9847</td><td>8476</td><td>9749</td><td>0982</td><td>8969</td><td>0247</td><td>89</td></tr>
            <tr className={this.state.rowStatus[4]}><td>685</td><td>3478</td><td>8</td><td>867</td><td>346</td><td>34</td><td>48</td><td>49</td><td>8</td><td>89</td><td>897</td><td>38</td><td></td></tr>
            <tr className={this.state.rowStatus[5]}><td>757</td><td>898990</td><td>8</td><td>200</td><td>285</td><td>923</td><td>9</td><td>387</td><td>238</td><td>578</td><td>875</td><td>87</td><td>9</td></tr>
          </tbody>
      </table>
    );
  }
}

export default Digits;
