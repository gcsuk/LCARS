import React, {Component} from 'react';
import {Link} from 'react-router';
import styled from 'styled-components';
import redAlertData from '../../data/alertCondition'
import redAlertStyles from '../../css/alertRed.css'

const Destruct = styled.div`
    text-align: center
`;

const DestructBarLarge = styled.div`
    height: 30px;
    width: 80%;
    margin: 15px 10%;
    background-color: #940606;
    animation: pulseBar1 1s infinite
`;

const DestructBarMedium = styled.div`
    height: 30px;
    width: 70%;
    margin: 15px 15%;
    background-color: #940606;
    animation: pulseBar2 1s infinite
`;

const DestructBarSmall = styled.div`
    height: 30px;
    width: 60%;
    margin: 15px 20%;
    background-color: #940606;
    animation: pulseBar3 1s infinite
`;

const DestructClock = styled.div`
    font-size: 100pt;
    color: #940606;
    line-height: 1;
    animation: pulseClock 1s infinite
`;

class RedAlertPage extends Component {
    constructor(props) {
      super(props);
  
      this.state = {alert: redAlertData};
    }
    render() {
      return (
        <Destruct>
            <DestructBarLarge></DestructBarLarge>
            <DestructBarMedium></DestructBarMedium>
            <DestructBarSmall></DestructBarSmall>
            <DestructClock>
                {this.state.alert.type} Alert<br />
                <span>{this.state.alert.hoursRemaining}</span> : <span>{this.state.alert.minutesRemaining}</span> : <span>{this.state.alert.secondsRemaining}</span> : <span>{this.state.alert.millisecondsRemaining}</span>
            </DestructClock>
            <DestructBarSmall></DestructBarSmall>
            <DestructBarMedium></DestructBarMedium>
            <DestructBarLarge></DestructBarLarge>
        </Destruct>
        );
    }
}

export default RedAlertPage;