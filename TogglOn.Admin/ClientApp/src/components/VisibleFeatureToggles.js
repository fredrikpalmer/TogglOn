import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/FeatureToggles';
import FeatureToggles from './FeatureToggles';

export default connect(
    state => {
        return { spinner: state.spinner, featureToggles: state.featureToggles }
    },
    dispatch => bindActionCreators(actionCreators, dispatch)
)(FeatureToggles);
