import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import deploymentData from '../../data/deployments'

const Deployments = styled.table`
  width: 100%;
  text-align: center;
  vertical-align: middle
`;

const ProjectName = styled.td`
  width: 150px;
  text-align: left
`;

const ProjectDeploy = styled.div`
  display: block;
  height: 40px;
  margin: 5px;
  font-size: 30px;
  border-radius: 20px;
  background-color: #015001
`;

class DeploymentsPage extends Component { 
  constructor(props) {
    super(props);

    this.state = {deployments: deploymentData};
  }

  render() {
    return (
      <Deployments>
        <thead>
          <th></th>
          { this.state.deployments[0].deploys.map((item, colIndex) => (
            <th key={colIndex}>
              {item.env}
            </th>
          ))}
        </thead>
        <tbody>
          { this.state.deployments.map((project, rowIndex) => (
            <tr key={rowIndex}>
              <ProjectName>
                {project.name}
              </ProjectName>
              { project.deploys.map((item, colIndex) => (
                <td key={colIndex}>
                  <ProjectDeploy>{item.version}</ProjectDeploy>
                </td>
              ))}
            </tr>)
          )}
        </tbody>
      </Deployments>
    );
  }
}

export default DeploymentsPage;