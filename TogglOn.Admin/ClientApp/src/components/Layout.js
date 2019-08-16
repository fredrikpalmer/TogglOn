import React from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from './NavMenu';
import VisibleSpinner from './VisibleSpinner';
import VisibleError from './VisibleError';

export default props => (
  <Grid fluid>
    <VisibleSpinner />
    <Row>
      <Col sm={3}>
        <NavMenu />
      </Col>
      <Col sm={9}>
        <VisibleError />

        {props.children}
      </Col>
    </Row>

  </Grid>
);
