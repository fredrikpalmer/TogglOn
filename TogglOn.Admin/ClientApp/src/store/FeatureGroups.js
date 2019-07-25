const requestFeatureGroupsType = 'REQUEST_FEATUREGROUPS';
const receiveFeatureGroupsType = 'RECEIVE_FEATUREGROUPS';
const initialState = { featureGroups: [], isLoading: false };

export const actionCreators = {
    requestFeatureGroups: () => async (dispatch) => {
        dispatch({ type: requestFeatureGroupsType });

        const url = "api/featuregroups";
        const response = await fetch(url);
        const featureGroups = await response.json();

        dispatch({ type: receiveFeatureGroupsType, featureGroups });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestFeatureGroupsType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveFeatureGroupsType) {
        return {
            ...state,
            featureGroups: action.featureGroups,
            isLoading: false
        };
    }

    return state;
};
