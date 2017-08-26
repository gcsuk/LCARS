import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';

const Environments = styled.table`
  width: 100%;
  text-align: center;
  vertical-align: middle
`;

const Environment = styled.tr`
  border-bottom: solid 1px #fff;
  font-size: 36px
`;

const SiteName = styled.td`
  width: 150px;
  text-align: left
`;

class EnvironmentsPage extends Component { 
  constructor(props) {
    super(props);
    
    const environments = new Array(5);

    for (var row = 0; row < 5; row++){
        environments[row] = {};
        environments[row].name = "Site " + row;
        environments[row].envs = new Array(5);
        for(var col = 0; col < 5; col++){ 
          environments[row].envs[col] = {};
          environments[row].envs[col].name = "ENV 01";
          environments[row].envs[col].status = "OK";
        }
    }

    this.state = {environments: environments};
  }

  render() {
    return (
      <Environments>
        <thead>
          <th></th>
          { this.state.environments[0].envs.map((item, colIndex) => (
            <th key={colIndex}>
              {item.name}
            </th>
          ))}
        </thead>
        <tbody>
          { this.state.environments.map((site, rowIndex) => (
            <Environment key={rowIndex}>
              <SiteName>
                {site.name}
              </SiteName>
              { site.envs.map((item, colIndex) => (
                <td key={colIndex}>
                  {item.status}
                </td>
              ))}
            </Environment>)
          )}
        </tbody>
      </Environments>
    );
  }
}

export default EnvironmentsPage;