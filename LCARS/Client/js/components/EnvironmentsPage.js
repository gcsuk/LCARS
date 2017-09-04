import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import environmentData from '../../data/environments'

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

    this.state = {environments: environmentData};
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