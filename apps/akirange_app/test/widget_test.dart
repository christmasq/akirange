// This is a basic Flutter widget test.
//
// To perform an interaction with a widget in your test, use the WidgetTester
// utility in the flutter_test package. For example, you can send tap and scroll
// gestures. You can also use WidgetTester to find child widgets in the widget
// tree, read text, and verify that the values of widget properties are correct.

import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';

import 'package:akirange_app/main.dart';

void main() {
  testWidgets('Shows bottom navigation tabs', (WidgetTester tester) async {
    await tester.pumpWidget(const AkiRangeApp());

    final navFinder = find.byType(BottomNavigationBar);
    expect(navFinder, findsOneWidget);

    expect(find.descendant(of: navFinder, matching: find.text('今日')), findsOneWidget);
    expect(find.descendant(of: navFinder, matching: find.text('本週')), findsOneWidget);
    expect(find.descendant(of: navFinder, matching: find.text('任務')), findsOneWidget);
  });
}
