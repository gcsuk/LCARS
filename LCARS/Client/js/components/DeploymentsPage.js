import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';

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
    
    const deployments = new Array(5);

    for (var row = 0; row < 5; row++){
        deployments[row] = {};
        deployments[row].name = "Project " + row;
        deployments[row].deploys = new Array(5);
        for(var col = 0; col < 5; col++){ 
            deployments[row].deploys[col] = {};
            deployments[row].deploys[col].env = "ENV 01";
            deployments[row].deploys[col].status = "OK";
            deployments[row].deploys[col].version = "1.0.1.2"
        }
    }

    this.state = {deployments: deployments};
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