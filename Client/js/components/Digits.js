import React from 'react';

class Digits extends React.Component {
  constructor(props) {
    super(props);
    
    const digits = new Array(props.rowCount);

    for (var row = 0; row < props.rowCount; row++){
        digits[row] = new Array(props.colCount);
        for(var col = 0; col < props.colCount; col++){ 
            digits[row][col] = Math.floor(Math.random() * ((row + 1) * 1000));
        }
    }

    this.state = {rowStatus: new Array(props.rowCount), digits: digits};
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
            { this.state.digits.map((row, index) => (
              <tr key={index} className={this.state.rowStatus[index]}>
                { row.map((col, index) => (
                  <td key={index}>{col}</td>
                ))}
              </tr>)
            )}
          </tbody>
      </table>
    );
  }
}

export default Digits;
