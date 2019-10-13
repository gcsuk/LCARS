import React, { useState, useCallback } from 'react';
import useInterval from 'use-interval';

const generateDigits = (rowCount, colCount) => {
  const digits = new Array(rowCount);

  for (let row = 0; row < rowCount; row++){
      digits[row] = new Array(colCount);
      for(let col = 0; col < colCount; col++){ 
          digits[row][col] = Math.floor(Math.random() * ((row + 1) * 1000));
      }
  }

  return digits;
};

export default function Digits(props) {

  const [rowStatus, setRowStatus] = useState(new Array(props.rowCount));
  const [digits] = useState(generateDigits(props.rowCount, props.colCount));

  const pickRows = useCallback(() => {
    const selectedRow = Math.floor(Math.random() * props.rowCount);

    const newState = JSON.parse(JSON.stringify(rowStatus));

    for (let i = 0, len = newState.length; i < len; i++) {
      newState[i] = "";
    }

    newState[selectedRow] = "white";
    newState[selectedRow + 1] = "white";

    setRowStatus(newState);
  }, [rowStatus, setRowStatus, props.rowCount]);

  useInterval(() => {
    pickRows();
  }, 1000);

  return (
    <table>
        <tbody>
          {digits.map((row, index) => (
            <tr key={index} className={rowStatus[index]}>
              { row.map((col, index) => (
                <td key={index}>{col}</td>
              ))}
            </tr>)
          )}
        </tbody>
    </table>
  );
}