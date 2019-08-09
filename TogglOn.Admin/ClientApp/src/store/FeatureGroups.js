import { toggleLoadingType } from './Loading';

const requestFeatureGroupsType = 'REQUEST_FEATUREGROUPS';
const receiveFeatureGroupsType = 'RECEIVE_FEATUREGROUPS';
const initialState = { featureGroups: [] };

export const actionCreators = {
    requestFeatureGroups: () => async (dispatch) => {
        dispatch({ type: requestFeatureGroupsType });
        dispatch({ type: toggleLoadingType, active: true });

        const url = "api/featuregroups";
        const response = await fetch(url);
        const featureGroups = await response.json();

        dispatch({ type: receiveFeatureGroupsType, featureGroups });
        dispatch({ type: toggleLoadingType, active: false });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestFeatureGroupsType) {
        return {
            ...state
        };
    }

    if (action.type === receiveFeatureGroupsType) {
        return {
            ...state,
            featureGroups: action.featureGroups
        };
    }

    return state;
};
