import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/FeatureGroups';
import FeatureGroups from './FeatureGroups';

export default connect(
    state => {
        return { featureGroups: state.featureGroups };
    },
    dispatch => bindActionCreators(actionCreators, dispatch)
)(FeatureGroups);
