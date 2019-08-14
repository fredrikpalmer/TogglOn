import React, { Component } from 'react';
import { connect } from 'react-redux';

class Error extends Component {
    render() {
        const { errors } = this.props;

        if (!errors.length) {
            return null;
        }

        return (
            <div className="alert alert-danger" role="alert">
                {this.props.errors.map(error => {
                    return (
                    <div key={error.message}>
                        <strong>Oops!</strong>
                        <p>{error.message}</p>
                    </div>
                    );
                })}
            </div>
            );
        }
    }

export default connect(
    state => {
        return { errors: state.errors };
    }
)(Error);
