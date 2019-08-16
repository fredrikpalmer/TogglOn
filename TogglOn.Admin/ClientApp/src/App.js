import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import VisibleFeatureToggles from './components/VisibleFeatureToggles';
import VisibleFeatureGroups from './components/VisibleFeatureGroups';

export default () => (
    <Layout>
        <Route exact path='/' component={VisibleFeatureToggles} />
        <Route path='/featuregroups' component={VisibleFeatureGroups} />
    </Layout>
);
