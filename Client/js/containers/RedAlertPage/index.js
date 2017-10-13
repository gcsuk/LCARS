//@flow

import React, {Component} from 'react';
import {Link} from 'react-router-dom';
import styled from 'styled-components';
import { connect } from 'react-redux';
import { mapStateToProps, mapDispatchToProps } from './state';
import redAlertStyles from '../../../css/alertRed.css'

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

export function getTime(endDate: number = 0) {
    const totalMilliseconds = new Date(endDate) - new Date();
    const totalSeconds = totalMilliseconds / 1000;
    const totalMinutes = totalSeconds / 60;
    const totalHours = totalMinutes / 60;
    const days = Math.floor(totalHours / 24);
    const hours = Math.floor(totalHours % 24);
    const minutes = Math.floor(totalMinutes % 60);
    const seconds = Math.floor(totalSeconds % 60);
    const milliseconds = Math.floor(totalMilliseconds % 1000);

    return {
        days,
        hours,
        hoursString: hours < 10 ? `0${hours}` : hours,
        minutes,
        minutesString: minutes < 10 ? `0${minutes}` : minutes,
        seconds,
        secondsString: seconds < 10 ? `0${seconds}` : seconds,
        milliseconds,
        millisecondsString: milliseconds < 10 ? `00${milliseconds}` : milliseconds < 100 ? `0${milliseconds}` : milliseconds
    };
}

class RedAlertPage extends Component {
    constructor(props) {
        super(props);

        this.state = {
            hoursRemaining: 0,
            minutesRemaining: 0,
            secondsRemaining: 0,
            millisecondsRemaining: 0
        }
    }
    componentWillMount() {
        this.intervalID = setInterval(() => this.tick(), 47);
    }
    componentWillUnmount() {
        clearInterval(this.intervalID);
    }
    tick() {
        const remaining = getTime(this.props.alertCondition.endDate);

        this.setState({
            hoursRemaining: remaining.hoursString,
            minutesRemaining: remaining.minutesString,
            secondsRemaining: remaining.secondsString,
            millisecondsRemaining: remaining.millisecondsString
        });
    }
    render() {
      return (
        <Destruct>
            <DestructBarLarge></DestructBarLarge>
            <DestructBarMedium></DestructBarMedium>
            <DestructBarSmall></DestructBarSmall>
            <DestructClock>
                {this.props.alertCondition.type} Alert<br />
                <span>{this.state.hoursRemaining}</span> : <span>{this.state.minutesRemaining}</span> : <span>{this.state.secondsRemaining}</span> : <span>{this.state.millisecondsRemaining}</span>
            </DestructClock>
            <DestructBarSmall></DestructBarSmall>
            <DestructBarMedium></DestructBarMedium>
            <DestructBarLarge></DestructBarLarge>
        </Destruct>
        );
    }
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
  )(RedAlertPage);