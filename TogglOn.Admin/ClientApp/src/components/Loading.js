import React, { Component } from 'react';
import { connect } from 'react-redux';
import './Loading.css';

class Loading extends Component {
    constructor(props) {
        super(props);
        this.delayedActivate = this.delayedActivate.bind(this);
        this.delayedDeactivate = this.delayedDeactivate.bind(this);
        this.setActive = this.setActive.bind(this);
        this.clearTimers = this.clearTimers.bind(this);

        this.state = {
            active: false
        }

        this.timers = [];
    }

    componentDidUpdate(prevProps) {
        if (this.props.active !== prevProps.active) {
            if (this.props.active) {
                this.delayedActivate(250);
            } else {
                this.delayedDeactivate(1000);
            }
        }
    }

    delayedActivate(delay) {
        this.timers.push(setTimeout(function() {
            if (this.props.active) {
                this.setActive(true);
            } 

        }.bind(this), delay));
    }

    delayedDeactivate(delay) {
        this.timers.push(setTimeout(function () {
            if (this.state.active) {
                this.setActive(false);
            }

            this.clearTimers();
        }.bind(this), delay));
    }

    clearTimers() {
        for (let i = 0; i < this.timers.length; i++) {
            clearTimeout(this.timers[i]);
        }

        this.timers.splice(0, this.timers.length);
    }

    setActive(active) {
        this.setState({ active: active });
    }

    render() {
        const { active } = this.state;

        if (!active) {
            return null;
        }

        return (
                <div className="loading-dots">
                    <div className="loading-dots--dot"></div>
                    <div className="loading-dots--dot"></div>
                    <div className="loading-dots--dot"></div>
                </div>
            );
        }
    }

export default connect(
    state => state.loading
)(Loading);
