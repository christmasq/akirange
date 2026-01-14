import 'package:flutter/material.dart';

void main() {
  runApp(const AkiRangeApp());
}

class AkiRangeApp extends StatelessWidget {
  const AkiRangeApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'AkiRange',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.indigo),
      ),
      home: const MainShell(),
    );
  }
}

class MainShell extends StatefulWidget {
  const MainShell({super.key});

  @override
  State<MainShell> createState() => _MainShellState();
}

class _MainShellState extends State<MainShell> {
  int _currentIndex = 0;

  static const _tabs = <_TabConfig>[
    _TabConfig('今日', Icons.today, HomePage()),
    _TabConfig('本週', Icons.event_note, PlanPage()),
    _TabConfig('任務', Icons.check_circle_outline, TasksPage()),
  ];

  @override
  Widget build(BuildContext context) {
    final current = _tabs[_currentIndex];
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
        title: Text(current.title),
      ),
      body: current.page,
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: _currentIndex,
        onTap: (index) => setState(() => _currentIndex = index),
        items: _tabs
            .map(
              (tab) => BottomNavigationBarItem(
                icon: Icon(tab.icon),
                label: tab.title,
              ),
            )
            .toList(),
      ),
    );
  }
}

class _TabConfig {
  const _TabConfig(this.title, this.icon, this.page);

  final String title;
  final IconData icon;
  final Widget page;
}

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return _PlaceholderPage(
      title: '今日',
      subtitle: '查看今天的排程與重點任務。',
    );
  }
}

class PlanPage extends StatelessWidget {
  const PlanPage({super.key});

  @override
  Widget build(BuildContext context) {
    return _PlaceholderPage(
      title: '本週',
      subtitle: '預覽本週可用時段與計畫。',
    );
  }
}

class TasksPage extends StatelessWidget {
  const TasksPage({super.key});

  @override
  Widget build(BuildContext context) {
    return _PlaceholderPage(
      title: '任務',
      subtitle: '管理待辦並快速安排進行。',
    );
  }
}

class _PlaceholderPage extends StatelessWidget {
  const _PlaceholderPage({
    required this.title,
    required this.subtitle,
  });

  final String title;
  final String subtitle;

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Padding(
        padding: const EdgeInsets.all(24),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(
              title,
              style: Theme.of(context).textTheme.headlineMedium,
            ),
            const SizedBox(height: 12),
            Text(
              subtitle,
              textAlign: TextAlign.center,
              style: Theme.of(context).textTheme.bodyLarge,
            ),
          ],
        ),
      ),
    );
  }
}
